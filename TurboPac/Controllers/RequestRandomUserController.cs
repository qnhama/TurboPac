

using TurboPac.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TurboPac.Controllers
{
    public class RequestRandomUserController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://randomuser.me/api/";

        private UltrapacDataEntities db = new UltrapacDataEntities();

        public async Task<ActionResult> Request()
        {
            RandomUser EmpInfo = new RandomUser();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<RandomUser>(EmpResponse);
                }

            }

            foreach (var result in EmpInfo.results)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Guid.NewGuid();
                usuario.Title = result.name.title;
                usuario.First = result.name.first;
                usuario.Last = result.name.last;
                usuario.Email = result.email;
                usuario.City = result.location.city;
                usuario.Name = result.location.street.name;
                usuario.Number = result.location.street.number;
                usuario.City = result.location.city;
                usuario.State = result.location.state;
                usuario.Country = result.location.country;
                usuario.Postcode = int.Parse(result.location.postcode);
                usuario.Gender = result.gender;
                db.Usuario.Add(usuario);

            }
            db.SaveChanges();
            return RedirectToAction("Index", "Usuarios");
        }
    }
}