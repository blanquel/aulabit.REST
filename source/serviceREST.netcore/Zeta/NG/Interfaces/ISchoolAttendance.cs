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
    public interface ISchoolAttendance
    {
        Message Create(SchoolAttendaceVM objSchoolAttendance);
        Message Update(SchoolAttendaceVM objSchoolAttendance);
        Message Delete(int id);
        Message GetbyID(int id);
    }

    public class SchoolAttendance : ISchoolAttendance
    {
        public Message Create(SchoolAttendaceVM objSAVM)
        {
            var res = new Message();

            try
            {
                SCHOOL_ATTENDANCE viewModel = new SCHOOL_ATTENDANCE { ATTENDANCE_RECORD = objSAVM.AttendanceRecord, STATUS_ITEM = true, CREATE_DATE = DateTime.Now, ID_SCHOOL_SUBJECTS = objSAVM.IdSchoolSubject, MAKER = objSAVM.MAKER, MODIFICATION_DATE = DateTime.Now };

                using (var context = new ModeloAula())
                {
                    var vRegister = context.SCHOOL_ATTENDANCE.AsQueryable().Where(x => x.ID_SCHOOL_SUBJECTS == objSAVM.IdSchoolSubject).SingleOrDefault();
                    if (vRegister != null)
                        throw new Exception(string.Format("* Ya se encuentra asignado la asistencia "));

                    context.Entry(viewModel).State = EntityState.Added;
                    context.SaveChanges();

                    objSAVM.Id = viewModel.ID;
                    res.status_item = true;
                    res.details = JsonConvert.SerializeObject(objSAVM, Formatting.None);
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
                    var objschoolattendance = context.SCHOOL_ATTENDANCE.AsQueryable().Where(x => x.ID == id).SingleOrDefault();

                    if (objschoolattendance != null)
                    {
                        objschoolattendance.STATUS_ITEM = false;
                        objschoolattendance.MODIFICATION_DATE = DateTime.Now;
                    }
                    else
                        throw new Exception("* Materia escolar con esta asintencia no existe, favor de veríficar");

                    context.Entry(objschoolattendance).State = EntityState.Modified;
                    context.SaveChanges();

                    res.status_item = true;
                    res.details = "Borrado con éxito";
                    res.details = JsonConvert.SerializeObject(res, Formatting.None);
                }

            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al borrar la asistencia Materia Escolar detalles:{0}", ex.Message);
            }
            return res;
        }

        public Message GetbyID(int id)
        {
            var res = new Message();
            try
            {
                using (var context = new ModeloAula())
                {
                    if (id == -10)
                    {
                        var objschoolattendance = context.SCHOOL_ATTENDANCE.AsQueryable().ToList();
                        List<SchoolAttendaceVM> viewModel = new List<SchoolAttendaceVM>();

                        foreach (var item in objschoolattendance)
                        {
                            var tmp = new SchoolAttendaceVM { Id = item.ID, AttendanceRecord = item.ATTENDANCE_RECORD, IdSchoolSubject = item.ID_SCHOOL_SUBJECTS, MAKER = item.MAKER, StatusItem = item.STATUS_ITEM };
                            viewModel.Add(tmp);
                        }

                        if (viewModel.Count > 0)
                        {

                            res.status_item = true;
                            res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                        }
                        else
                        {
                            res.status_item = false;
                            res.details = "* Sin registros";
                        }
                    }
                    else
                    {

                        var objschoolattendance = context.SCHOOL_ATTENDANCE.AsQueryable().Where(x => x.ID == id).SingleOrDefault();

                        if (objschoolattendance != null)
                        {
                            var viewModel = new SchoolAttendaceVM { Id = objschoolattendance.ID, AttendanceRecord = objschoolattendance.ATTENDANCE_RECORD, IdSchoolSubject = objschoolattendance.ID_SCHOOL_SUBJECTS, MAKER = objschoolattendance.MAKER, StatusItem = objschoolattendance.STATUS_ITEM };
                            res.status_item = true;
                            res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                        }
                        else
                        {
                            res.status_item = false;
                            res.details = String.Format("* Incidencia veríficar la asistencia en esta materia escolar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status_item = false;
                res.details = String.Format("* Incidencia en el modulo la Asistencia de Materia Escolar:{0}", ex.Message);
            }
            return res;
        }

        public Message Update(SchoolAttendaceVM objSchoolAttendance)
        {
            var res = new Message();
            try
            {

                using (var context = new ModeloAula())
                {
                    var vobjSchoolAttendance = context.SCHOOL_ATTENDANCE.AsQueryable().Where(x => x.ID == objSchoolAttendance.Id).SingleOrDefault();

                    if (vobjSchoolAttendance != null)
                    {
                        vobjSchoolAttendance.ATTENDANCE_RECORD = objSchoolAttendance.AttendanceRecord;
                        vobjSchoolAttendance.MODIFICATION_DATE = DateTime.Now;
                        vobjSchoolAttendance.MAKER = objSchoolAttendance.MAKER;
                    }
                    else
                        throw new Exception(string.Format("* Asistencia {0} No existe", objSchoolAttendance.Id));

                    context.Entry(vobjSchoolAttendance).State = EntityState.Modified;
                    context.SaveChanges();

                    res.status_item = true;
                    res.details = string.Format("* Se han realizado los cambios correctamente de la asistencia {0}", objSchoolAttendance.Id);
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
