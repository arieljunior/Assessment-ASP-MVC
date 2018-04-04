using Data.Context;
using DomainModel.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private string Url;
        public AuthorController()
        {
            Url = "http://localhost:59061/api/Author/";
        }

        // GET: Author
        public ActionResult ListAuthors()
        {
            var Authors = new List<AuthorViewModel>();
            var requisicaoWeb = WebRequest.CreateHttp(Url);
            requisicaoWeb.Method = "GET";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<List<Author>>(objResponse.ToString());
                foreach (var a in post)
                {
                    var author = new AuthorViewModel()
                    {
                        Id = a.AuthorId,
                        Name = a.Name,
                        LastName = a.LastName,
                        Email = a.Email,
                        DateBirth = a.DateBirth
                    };

                    Authors.Add(author);
                }
                streamDados.Close();
                resposta.Close();
            }

            //HttpClient cliente = new HttpClient();
            //Task<HttpResponseMessage> resultado = cliente.GetAsync(Url);

            //Task<List<Author>> taskProfile = resultado.Result.Content.ReadAsAsync<List<Author>>();
            //List<Author> authors = taskProfile.Result;

            //foreach(var a in authors)
            //{
            //    var author = new AuthorViewModel()
            //    {
            //        Email = a.Email,
            //        DateBirth = a.DateBirth,
            //        LastName = a.LastName,
            //        Name = a.Name,
            //        Id = a.AuthorId
            //    };

            //    Authors.Add(author);
            //}


            return View(Authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var Author = new AuthorViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Author>(objResponse.ToString());
                Author.Id = data.AuthorId;
                Author.LastName = data.LastName;
                Author.Name = data.Name;
                Author.Email = data.Email;
                Author.DateBirth = data.DateBirth;
                streamDados.Close();
                resposta.Close();
            }

            return View(Author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(AuthorViewModel author)
        {
            try
            {
                //bool isSave;
                string dadosPOST = $"Name={author.Name}&LastName={author.LastName}&Email={author.Email}" +
                    $"&DateBirth={author.DateBirth.Year}/{author.DateBirth.Month}/{author.DateBirth.Day}";
                var dados = Encoding.UTF8.GetBytes(dadosPOST);
                var requisicaoWeb = WebRequest.CreateHttp(Url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;

                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();
                }

                //using (var resposta = requisicaoWeb.GetResponse())
                //{
                //    HttpWebResponse resp = (HttpWebResponse)requisicaoWeb.GetResponse();
                //    HttpStatusCode respStatusCode = resp.StatusCode;
                //    //isSave = respStatusCode.Equals("OK") ? true : false;
                //    resposta.Close();
                //}
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    //StreamReader reader = new StreamReader(streamDados);
                    //object objResponse = reader.ReadToEnd();
                    streamDados.Close();
                    resposta.Close();
                }

                return RedirectToAction("ListAuthors");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var Author = new AuthorViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Author>(objResponse.ToString());
                Author.Id = data.AuthorId;
                Author.LastName = data.LastName;
                Author.Name = data.Name;
                Author.Email = data.Email;
                Author.DateBirth = data.DateBirth;
                streamDados.Close();
                resposta.Close();
            }

            return View(Author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AuthorViewModel author)
        {
            try
            {
                var Author = new Author()
                {
                    AuthorId = author.Id,
                    DateBirth = author.DateBirth,
                    Email = author.Email,
                    LastName = author.LastName,
                    Name = author.Name
                };

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:59061/");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> response = client.PutAsJsonAsync("api/Author/" + id, Author);
                if (response.Result.IsSuccessStatusCode)
                    return RedirectToAction("ListAuthors");

                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var Author = new AuthorViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Author>(objResponse.ToString());
                Author.Id = data.AuthorId;
                Author.LastName = data.LastName;
                Author.Name = data.Name;
                Author.Email = data.Email;
                Author.DateBirth = data.DateBirth;
                streamDados.Close();
                resposta.Close();
            }
            return View(Author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                WebRequest request = WebRequest.Create(Url + id);
                request.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                return RedirectToAction("ListAuthors");
            }
            catch
            {
                return View();
            }
        }
    }
}
