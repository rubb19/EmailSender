using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emailSender.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text;
using System.Data.SqlClient;


namespace emailSender.Controllers
{

	public class SenderController : Controller
	{
		

		// GET: Sender
		// Show data from table
		public ActionResult SendMail() //Dropdown
		{
			return View();
		
		}



		// Set data into database
		public ActionResult SetDataInDataBase()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SetDataInDataBase(info model)
		{
			info tbl = new info();
			tbl.nombre = model.nombre;
			tbl.a_paterno = model.a_paterno;
			tbl.a_materno = model.a_materno;
			tbl.email = model.email;
			db.info.Add(tbl);
			db.SaveChanges();
			return View();
		}
		



		// Eliminar registro
		public ActionResult Delete(int id)
		{
			//var item = db.LoginPanel.Where(x => x.ID == id).First();
			var item = db.info.FirstOrDefault(x => x.id.Equals(id));
			if (item != null)
			{
				db.info.Remove(item);
				db.SaveChanges();
			}
			var item2 = db.info.ToList();
			return View("SendMail", item2);
		}




		// Editar registro
		public ActionResult Edit(int id)
		{
			var item = db.info.Where(x => x.id == id).First();
			return View(item);
		}
		[HttpPost]

		// Editar registro
		public ActionResult Edit(info model)
		{
			var item = db.info.Where(x => x.id == model.id).First();
			item.nombre = model.nombre;
			item.a_paterno = model.a_paterno;
			item.a_materno = model.a_materno;
			item.email = model.email;
			db.SaveChanges();
			var item2 = db.info.ToList();
			return View("SendMail", item2);
		}





		usuarioEntities db = new usuarioEntities();
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Contact(EmailFormModel model)
		{
			if (ModelState.IsValid)
			{
				IQueryable<info> Datos = db.info;
				var mensaje = Datos.Where(info => info.id == model.id).FirstOrDefault();
				
				var body = "<p>Email To: {0} {1} {2} ({3})</p><p>Message:</p><p>{4}</p>";
				var message = new MailMessage();
				message.To.Add(new MailAddress("rubbanaya@gmail.com")); //replace with valid value
				message.To.Add(new MailAddress(mensaje.email)); //replace with valid value
				message.Subject = "Your email subject";
				message.Body = string.Format(body, mensaje.nombre, mensaje.a_paterno, mensaje.a_materno, mensaje.email, model.Message);
				message.IsBodyHtml = true;
				using (var smtp = new SmtpClient())
				{
					var credential = new NetworkCredential
					{
						UserName = "",  // replace with gmail valid value
						Password = ""  // replace with gmail valid value
					};
					smtp.Credentials = credential;
					smtp.Host = "smtp.gmail.com";
					smtp.Port = 587;
					smtp.EnableSsl = true;

					try
					{
						smtp.Send(message);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
							ex.ToString());
					}
					smtp.Send(message);
					return RedirectToAction("Sent");


				}
			}
			return View(model);

		}
		public ActionResult Contact()
		{
			List<info> UserList = db.info.ToList();
			ViewBag.UserList = new SelectList(UserList, "id", "nombre");
			return View();
		}

		public ActionResult Sent()
		{
			return View();
		}

	}
}