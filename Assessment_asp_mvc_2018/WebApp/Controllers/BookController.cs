using DomainModel.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private string Url;
        public BookController()
        {
            Url = "http://localhost:59061/api/Book/";
        }
        // GET: Book
        public ActionResult ListBooks()
        {
            var Books = new List<BookViewModel>();
            var requisicaoWeb = WebRequest.CreateHttp(Url);
            requisicaoWeb.Method = "GET";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var booksData = JsonConvert.DeserializeObject<List<Book>>(objResponse.ToString());
                foreach (var b in booksData)
                {
                    var book = new BookViewModel()
                    {
                        Id = b.BookId,
                        Title = b.Title,
                        Isbn = b.Isbn,
                        Year = b.Year
                    };

                    //if (b.Authors != null)
                    //{
                    //    foreach (var ab in b.Authors)
                    //    {
                    //        var author = new AuthorViewModel()
                    //        {
                    //            Name = ab.Name,
                    //            LastName = ab.LastName,
                    //            Email = ab.Email,
                    //            DateBirth = ab.DateBirth,
                    //            Id = ab.AuthorId
                    //        };

                    //        book.Authors.Add(author);
                    //    }
                    //}
                    Books.Add(book);

                }
                streamDados.Close();
                resposta.Close();
            }

            return View(Books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var Book = new BookViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Book>(objResponse.ToString());
                Book.Id = data.BookId;
                Book.Title = data.Title;
                Book.Isbn = data.Isbn;
                Book.Year = data.Year;
                //Book.Authors = data.Authors;
                streamDados.Close();
                resposta.Close();
            }
            return View(Book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            try
            {
                //string dadosPOST = $"Title={book.Title}&Isbn={book.Isbn}&Year={book.Year}";
                //var dados = Encoding.UTF8.GetBytes(dadosPOST);
                //var requisicaoWeb = WebRequest.CreateHttp(Url);
                //requisicaoWeb.Method = "POST";
                //requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                //requisicaoWeb.ContentLength = dados.Length;

                //using (var stream = requisicaoWeb.GetRequestStream())
                //{
                //    stream.Write(dados, 0, dados.Length);
                //    stream.Close();
                //}

                //using (var resposta = requisicaoWeb.GetResponse())
                //{
                //    HttpWebResponse resp = (HttpWebResponse)requisicaoWeb.GetResponse();
                //    HttpStatusCode respStatusCode = resp.StatusCode;
                //    //isSave = respStatusCode.Equals("OK") ? true : false;
                //    resposta.Close();
                //}

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:59061/");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/Book/", book);
                if (response.Result.IsSuccessStatusCode)
                    return RedirectToAction("ListBooks");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var Book = new BookViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Book>(objResponse.ToString());
                Book.Id = data.BookId;
                Book.Title = data.Title;
                Book.Isbn = data.Isbn;
                Book.Year = data.Year;
                //Book.Authors = data.Authors;
                streamDados.Close();
                resposta.Close();
            }

            return View(Book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookViewModel book)
        {
            try
            {
                var Book = new Book()
                {
                    BookId = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    Year = book.Year
                };
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:59061/");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> response = client.PutAsJsonAsync("api/Book/" + id, Book);
                if (response.Result.IsSuccessStatusCode)
                    return RedirectToAction("ListBooks");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var Book = new BookViewModel();
            var requisicaoWeb = WebRequest.CreateHttp(Url + id);
            requisicaoWeb.Method = "GET";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Book>(objResponse.ToString());
                Book.Id = data.BookId;
                Book.Title = data.Title;
                Book.Isbn = data.Isbn;
                Book.Year = data.Year;
                //Book.Authors = data.Authors;
                streamDados.Close();
                resposta.Close();
            }
            return View(Book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                WebRequest request = WebRequest.Create(Url + id);
                request.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return RedirectToAction("ListBooks");
            }
            catch
            {
                return View();
            }
        }
    }
}
