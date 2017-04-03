using AutoMapper;
using AutoMapper.Mappers;
using NG.FC_DB;
using NG.Models;
using NG.Utils;
using NG.ViewModels;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NG.Interfaces
{
    public interface IStudent
    {
        Message Login(LoginVM login);
        Message Create(StudentVM student);

        Message Update(StudentVM student);
        Message Delete(int id);
        Message GetbyID(int id);
    }

    public class Student : IStudent
    {
        public Message Login(LoginVM login)
        {
            var res = new Message();
            try
            {
                using (var context = new ModeloAula())
                {
                    var vGetaccesss = context.STUDENTs.AsQueryable().
                                                       Where(x => x.STATUS_ITEM == true && x.EMAIL == login.Email && x.PWD == login.PWD).FirstOrDefault<STUDENT>();
                    // TODO analizar lo del 
                    if (vGetaccesss != null)
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap<STUDENT, StudentVM>());
                        var viewModel = AutoMapper.Mapper.Map<StudentVM>(vGetaccesss);

                        res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                    }
                    else
                    {
                        res.details = "* Verificar usuario y/o contraseña";
                        res.details = JsonConvert.SerializeObject(res, Formatting.None);
                    }

                }
            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al obtener credenciales detalles: {0}", ex.Message);
            }
            return res;
        }

        public Message Create(StudentVM student)
        {
            var res = new Message();
            var email = new Email { To = new List<string> { student.Email }, typeEmail = TypeEmail.CREATE };
            try
            {
                student.Token = "-1000";
                //student.Status = true;
                Mapper.Initialize(cfg => cfg.CreateMap<StudentVM, STUDENT>());
                var viewModel = AutoMapper.Mapper.Map<STUDENT>(student);
                viewModel.STATUS_ITEM = true;
                using (var context = new ModeloAula())
                {

                    if (!context.STUDENTs.Any(x => x.EMAIL == viewModel.EMAIL))
                    {
                        Task.Run(() => email.SendAsync());

                        context.Entry(viewModel).State = EntityState.Added;
                        context.SaveChanges();

                        student.ID = viewModel.ID;
                        res.status_item = true;
                        res.details = JsonConvert.SerializeObject(student, Formatting.None);
                    }
                    else
                    {
                        res.status_item = false;
                        res.details = string.Format("* El usuario {0} ya existe", viewModel.EMAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al crear usuario detalles:{0}", ex.Message);
            }

            Task.WaitAll();
            return res;
        }

        public Message Update(StudentVM student)
        {
            var res = new Message();
            try
            {
                if (student.ID > 0)
                {
                    using (var context = new ModeloAula())
                    {
                        var objstudent = context.STUDENTs.AsQueryable().Where(x => x.ID == student.ID).FirstOrDefault();

                        if (objstudent != null)
                        {
                            objstudent.NAME = student.Name;
                            objstudent.LASTNAME = student.LastName;
                            objstudent.PWD = student.PWD;
                            objstudent.EMAIL = student.Email;
                            objstudent.STATUS_ITEM = student.Status;
                            objstudent.MODIFICATION_DATE = DateTime.Now;
                            student.Token = objstudent.TOKEN = HashCore.Token();
                        }

                        context.Entry(objstudent).State = EntityState.Modified;
                        context.SaveChanges();

                        res.status_item = true;
                        res.details = JsonConvert.SerializeObject(student, Formatting.None);
                    }
                }
                else
                {
                    res.status_item = false;
                    res.details = String.Format("* Incidencia veríficar id usuario");
                }
            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al crear usuario detalles:{0}", ex.Message);
            }
            return res;
        }

        public Message Delete(int id)
        {
            var res = new Message();
            try
            {
                if (id > 0)
                {
                    using (var context = new ModeloAula())
                    {
                        var objstudent = context.STUDENTs.AsQueryable().Where(x => x.ID == id).FirstOrDefault();

                        if (objstudent != null)
                        {
                            objstudent.STATUS_ITEM = false;
                            objstudent.MODIFICATION_DATE = DateTime.Now;
                        }

                        context.Entry(objstudent).State = EntityState.Modified;
                        context.SaveChanges();

                        res.status_item = true;
                        res.details = "Borrado con éxito";
                        res.details = JsonConvert.SerializeObject(res, Formatting.None);
                    }
                }
                else
                {
                    res.status_item = false;
                    res.details = String.Format("* Incidencia veríficar id usuario");
                }
            }
            catch (Exception ex)
            {
                res.details = String.Format("* Incidencia al borrar usuario detalles:{0}", ex.Message);
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
                        var objstudent = context.STUDENTs.AsQueryable().ToList();

                        Mapper.Initialize(cfg => cfg.CreateMap<STUDENT, StudentVM>());
                        //var viewModel = Mapper.Map<StudentVM>(objstudent);

                        List<StudentVM> viewModel = Mapper.Map<List<STUDENT>, List<StudentVM>>(objstudent);
                        res.status_item = true;
                        res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                    }
                    else
                    {
                        var objstudent = context.STUDENTs.AsQueryable().Where(x => x.ID == id).FirstOrDefault();

                        if (objstudent != null)
                        {
                            Mapper.Initialize(cfg => cfg.CreateMap<STUDENT, StudentVM>());
                            var viewModel = AutoMapper.Mapper.Map<StudentVM>(objstudent);
                            res.status_item = true;
                            res.details = JsonConvert.SerializeObject(viewModel, Formatting.None);
                        }
                        else
                        {
                            res.status_item = false;
                            res.details = String.Format("* Incidencia veríficar id usuario");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status_item = false;
                res.details = String.Format("* Incidencia al borrar usuario detalles:{0}", ex.Message);
            }
            return res;
        }
    }
}
