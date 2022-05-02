using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TurboPac.Models;

namespace TurboPac.Controllers
{
    public class UsuariosController : Controller
    {
        private UltrapacDataEntities db = new UltrapacDataEntities();

        private List<string> errores = new List<string>();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gender,Title,First,Last,Number,Name,City,State,Country,Postcode,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gender,Title,First,Last,Number,Name,City,State,Country,Postcode,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult upload(HttpPostedFileBase file)
        {
            bool result = false;
            StringBuilder strbuild = new StringBuilder();
            List<string> lines = new List<string>();

            try
            {
                if (file.ContentLength == 0)
                    throw new Exception("Zero length file!");
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Document"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/Document")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Document"));
                    }
                    file.SaveAs(filePath);
                    if (!string.IsNullOrEmpty(filePath))
                    {


                        
                        using (StreamReader sr = new StreamReader(Path.Combine(Server.MapPath("~/Document"), fileName)))
                        {
                            string ln;

                            while ((ln = sr.ReadLine()) != null)
                            {
                                lines.Add(ln);
                            }
                            sr.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                result = false;

            }

            //return new JsonResult()
            //{
            //    Data = strbuild.ToString()
            //};

            if (!saveTxtData(lines))
            {
                ViewBag.saveTxt = "El txt tiene errores";
            }
            return RedirectToAction("Index");

        }

        private bool saveTxtData(List<string> renglones)
        {
            string count = "";
            try
            {

                List<Usuario> usuarios = new List<Usuario>();

                foreach (var renglon in renglones)
                {
                    string[] valores = renglon.Split('|');

                    count = valores[0];
                    Usuario usuario = new Usuario();
                    string[] nombre = valores[1].Split(' ');
                    usuario.First = nombre[0];
                    usuario.Last = string.IsNullOrEmpty(valores[1]) ? "" : nombre[1];

                    usuario.Email = valores[2];

                    if (!string.IsNullOrEmpty(valores[3]))
                    {
                        string[] direccion = valores[3].Split(' ');
                        usuario.Number = !string.IsNullOrWhiteSpace(direccion[0]) ? int.Parse(direccion[0]) : 0;
                        usuario.Name = direccion[1] + " " + direccion[2];

                    }
                    else
                    {
                        usuario.Number = 0;
                        usuario.Name = string.Empty;

                    }

                    usuario.Gender = valores[4];
                    usuario.Id = Guid.NewGuid();
                    usuarios.Add(usuario);
                }
                db.Usuario.AddRange(usuarios);

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                errores.Add("El renglon " + count + "tiene errores.");
                if (e != null)
                {
                    errores.Add(e.Message + " " + e.InnerException);

                }
                return false;

            }

        }
    }
}
