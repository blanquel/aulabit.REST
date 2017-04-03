using System;
using System.Collections.Generic;
using NG.Models;
using NG.ViewModels;
using AutoMapper;
using NG.FC_DB;
using System.Linq;
using System.Data.Entity;
using Newtonsoft.Json;

namespace NG.Interfaces
{
    public interface ISchoolSubject
    {
        Message Create(SchoolSubjectVM objSchoolSubject);
        Message Update(SchoolSubjectVM objSchoolSubject);
        Message Delete(int id);
        Message GetbyID(int id);
    }

    public class SchoolSubject : ISchoolSubject
    {
        public Message Create(SchoolSubjectVM objSchoolSubject)
        {
            var res = new Message();

            try
            {
                // Convert to object required
                Mapper.Initialize(cfg => cfg.CreateMap<SchoolSubjectVM, SCHOOL_SUBJECTS>().ForMember(x => x.ID_CAT_DET, y => y.MapFrom(c => c.IdCatDet))
                                                                                          .ForMember(x => x.RATING_RECORD, y => y.MapFrom(c => c.RatingRecord))
                                                                                          .ForMember(x => x.ID_STUDENT, y => y.MapFrom(c => c.IdStudent)));

                var viewModel = AutoMapper.Mapper.Map<SCHOOL_SUBJECTS>(objSchoolSubject);

                using (var context = new ModeloAula())
                {
                    // TODO validate if exist School Subject 
                    var vSchoolSubject = context.CATALOG_DETAILS.AsQueryable().Where(x => x.ID_CATALOG_DEFINITION == (int)CATALOG_TABLES.SCHOOL_SUBJECTS && x.ROW_ITEM == objSchoolSubject.IdCatDet).SingleOrDefault();
                    if (vSchoolSubject == null)
                        throw new Exception("* Debe elegir una materia escolar Hábil");

                    // TODO validate if exist Student
                    var vStudent = context.STUDENTs.AsQueryable().Where(x => x.ID == objSchoolSubject.IdStudent && x.STATUS_ITEM == true).SingleOrDefault();
                    if (vStudent == null)
                        throw new Exception("* Debe elegir un estudiante Hábil");

                    // TODO validate if exist Register

                    var vRegister = context.SCHOOL_SUBJECTS.AsQueryable().Where(x => x.ID_CAT_DET == objSchoolSubject.IdCatDet && x.ID_STUDENT == objSchoolSubject.IdStudent).SingleOrDefault();
                    if (vRegister != null)
                        throw new Exception(string.Format("* Ya se encuentra asignado {0} para {1}", vSchoolSubject.FIELD0, vStudent.NAME));

                    context.Entry(viewModel).State = EntityState.Added;
                    context.SaveChanges();

                    objSchoolSubject.Id = viewModel.ID;
                    res.status_item = true;
                    res.details = JsonConvert.SerializeObject(objSchoolSubject, Formatting.None);
                }
            }
            catch (Exception ex)
            {
                res.status_item = false;
                res.details = String.Format("* Incidencia al crear Materia Escolar detalles:{0}", ex.Message);
            }
            return res;
        }

        public Message Delete(int id)
        {
            var res = new Message();
            try
            {
                using (var context = new ModeloAula())
                {
                    var objschoolsubject = context.SCHOOL_SUBJECTS.AsQueryable().Where(x => x.ID == id).FirstOrDefault();

                    if (objschoolsubject != null)
                    {
                        objschoolsubject.STATUS_ITEM = false;
                        objschoolsubject.MODIFICATION_DATE = DateTime.Now;
                    }
                    else
                        throw new Exception("* Materia escolar no existe, favor de veríficar");

                    context.Entry(objschoolsubject).State = EntityState.Modified;
                    context.SaveChanges();

                    res.status_item = true;
                    res.details = "Borrado con éxito";
                    res.details = JsonConvert.SerializeObject(res, Formatting.None);
                }

            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al borrar Materia Escolar detalles:{0}", ex.Message);
            }
            return res;
        }

        public Message GetbyID(int id)
        {
            var res = new Message();
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<SCHOOL_SUBJECTS, SchoolSubjectVM>().ForMember(x => x.IdCatDet, y => y.MapFrom(c => c.ID_CAT_DET))
                                                                                                       .ForMember(x => x.RatingRecord, y => y.MapFrom(c => c.RATING_RECORD))
                                                                                                       .ForMember(x => x.IdStudent, y => y.MapFrom(c => c.ID_STUDENT)));

                using (var context = new ModeloAula())
                {
                    if (id == -10)
                    {
                        var objschoolsubject = context.SCHOOL_SUBJECTS.AsQueryable().ToList();

                        
                        List<SchoolSubjectVM> viewModel = Mapper.Map<List<SCHOOL_SUBJECTS>, List<SchoolSubjectVM>>(objschoolsubject);
                        res.status_item = true;
                        res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                    }
                    else
                    {
                        
                        var objschoolsubject = context.SCHOOL_SUBJECTS.AsQueryable().Where(x => x.ID == id).SingleOrDefault();

                        if (objschoolsubject != null)
                        {

                            

                            var viewModel = Mapper.Map<SchoolSubjectVM>(objschoolsubject);
                            res.status_item = true;
                            res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                        }
                        else
                        {
                            res.status_item = false;
                            res.details = String.Format("* Incidencia veríficar id de la materia escolar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status_item = false;
                res.details = String.Format("* Incidencia en el modulo Materia Escolar:{0}", ex.Message);
            }
            return res;
        }

        public Message Update(SchoolSubjectVM objSchoolSubject)
        {
            var res = new Message();
            try
            {

                using (var context = new ModeloAula())
                {
                    var vobjSchoolSubject = context.SCHOOL_SUBJECTS.AsQueryable().Where(x => x.ID == objSchoolSubject.Id).SingleOrDefault();

                    if (vobjSchoolSubject != null)
                    {
                        vobjSchoolSubject.RATING_RECORD = objSchoolSubject.RatingRecord;
                        vobjSchoolSubject.MODIFICATION_DATE = DateTime.Now;
                        vobjSchoolSubject.MAKER = objSchoolSubject.MAKER;
                    }
                    else
                        throw new Exception("* Materia Escolar No existe");

                    context.Entry(vobjSchoolSubject).State = EntityState.Modified;
                    context.SaveChanges();

                    res.status_item = true;
                    res.details = "* Se han realizado los cambios correctamente";
                    res.details = JsonConvert.SerializeObject(res, Formatting.None);
                }

            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al modíficar información de la materia escolar detalles:{0}", ex.Message);
            }
            return res;
        }
    }
}
