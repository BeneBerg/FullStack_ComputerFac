using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using EntityState = System.Data.Entity.EntityState;


namespace API.Controllers
{
    public class ClientsController : ApiController
    {
        private ComputerBDEntities db = new ComputerBDEntities();
        public bool ClientCheck;

        // GET: api/Clients

        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }
        /// <summary>
        /// Получение данных по поиску имени клиента
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET: api/Clients/5
        [Route("api/ClientByName")]
        public IHttpActionResult GetClient(string name)
        {
            var client = db.Clients.ToList().Where(p => p.name.Contains(name)).ToList();
            return Ok(client);
        }
        /// <summary>
        /// получение всего списка клиентов
        /// </summary>
        /// <returns></returns>
        [Route("api/GetClients")]
        public IHttpActionResult GetAllClients()
        {
            var client = db.Clients.ToList();
            return Ok(client);
        }
      /// <summary>
      /// Изминение данных клиента
      /// </summary>
      /// <param name="id"></param>
      /// <param name="client"></param>
      /// <returns></returns>
        [Route("api/PutClients")]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.client_id)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }
        /// <summary>
        /// Добавление клиента 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.name))
                ModelState.AddModelError("Name", "Имя не может быть пустым");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.client_id }, client);
        }
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            
            if (client == null)
            {
                return NotFound();
            }
            var order = db.Orders.Where(p => p.client_id == id).ToList();
            if (order.Count > 0)
            {
                ModelState.AddModelError("Ошибка", "Нельзя удалить клиента т.к он связан");
                ClientCheck = true;
                return BadRequest(ModelState);
            }
            else ClientCheck = false;
                Clients_log clients_Log = new Clients_log { client_id = client.client_id, name = client.name, houseNumber = client.houseNumber, creationDate = DateTime.UtcNow };

            db.Clients.Remove(client);
           

            db.Clients_log.Add(clients_Log);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.client_id }, client);

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.client_id == id) > 0;
        }
    }
}