using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class appsController : ApiController
    {
        private appStore_MorozovEntities db = new appStore_MorozovEntities();

        //SORT: api/sort? flag = { flag }
        [Route("api/apps/sort")]
        [HttpGet]
        public async Task<IHttpActionResult> Sortapps(bool flag)
        {
            if (flag)
            {
                return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderBy(x => x.appAgeLimit));
            }
            else
            {
                return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderByDescending(x => x.appAgeLimit));
            }
        }

        [Route("api/apps/search")]
        [HttpGet]

        //searchAppNamee - название приложения, order - true(asc)/false(desc), field - 0(-)/1(price)/2(age limit)/3(name)
        public async Task<IHttpActionResult> Searchapps(string searchAppName, bool order, int field)
        {
            if(String.IsNullOrEmpty(searchAppName))
            {
                if (order)
                {
                    switch(field)
                    {
                        case 0:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)));
                            break;
                        case 1:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderBy(x => x.appPrice));
                            break;
                        case 2:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderBy(x => x.appAgeLimit));
                            break;
                        case 3:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderBy(x => x.appName));
                            break;
                        default:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)));
                            break;
                    }
                }
                else
                {
                    switch (field)
                    {
                        case 0:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)));
                            break;
                        case 1:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderByDescending(x => x.appPrice));
                            break;
                        case 2:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderByDescending(x => x.appAgeLimit));
                            break;
                        case 3:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).OrderByDescending(x => x.appName));
                            break;
                        default:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)));
                            break;
                    }
                }
            }
            else
            {
                if (order)
                {
                    switch (field)
                    {
                        case 0:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())));
                            break;
                        case 1:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderBy(x => x.appPrice));
                            break;
                        case 2:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderBy(x => x.appAgeLimit));
                            break;
                        case 3:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderBy(x => x.appName));
                            break;
                        default:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())));
                            break;
                    }
                }
                else
                {
                    switch (field)
                    {
                        case 0:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())));
                            break;
                        case 1:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderByDescending(x => x.appPrice));
                            break;
                        case 2:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderByDescending(x => x.appAgeLimit));
                            break;
                        case 3:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())).OrderByDescending(x => x.appName));
                            break;
                        default:
                            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)).Where(x => x.appName.ToLower().Contains(searchAppName.ToLower())));
                            break;
                    }
                }
            }
        }

        // GET: api/apps
        [ResponseType(typeof(List<modelApp>))]
        public IHttpActionResult Getapps()
        {
            return Ok(db.apps.ToList().ConvertAll(x => new modelApp(x)));
        }

        // GET: api/apps/5
        [ResponseType(typeof(apps))]
        public IHttpActionResult Getapps(int id)
        {
            apps apps = db.apps.Find(id);
            if (apps == null)
            {
                return NotFound();
            }

            return Ok(apps);
        }

        // PUT: api/apps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapps(int id, apps apps)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apps.app_id)
            {
                return BadRequest();
            }

            db.Entry(apps).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/apps
        [ResponseType(typeof(apps))]
        public IHttpActionResult Postapps(apps apps)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.apps.Add(apps);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apps.app_id }, apps);
        }

        // DELETE: api/apps/5
        [ResponseType(typeof(apps))]
        public IHttpActionResult Deleteapps(int id)
        {
            apps apps = db.apps.Find(id);
            if (apps == null)
            {
                return NotFound();
            }

            db.apps.Remove(apps);
            db.SaveChanges();

            return Ok(apps);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appsExists(int id)
        {
            return db.apps.Count(e => e.app_id == id) > 0;
        }
    }
}