using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginFunctionality.Models;
using LoginFunctionality.Models.Patient;
using LoginFunctionality.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUtilities;

namespace LoginFunctionality.Controllers
{
    public class PatientController : Controller
    {
        private IApiClient apiProxy;
        private ILogger<PatientController> _logger;

        private const string CLAIM_TYPE_NAME =
           "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public PatientController(IApiClient apiClient, ILogger<PatientController> logger)
        {
            apiProxy = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            PatientViewModel objModel = new PatientViewModel();
            List<AppointmentListDoctor> lstdoctor = RedisConnector.Get<List<AppointmentListDoctor>>(User.Identity.Name + "Appointmentlstdoctor");
            if (lstdoctor == null)
            {
                lstdoctor = await apiProxy.GetListAsync<AppointmentListDoctor>("patient/GetDoctors");
                //Saving a list to redis using object
                RedisConnector.Set(User.Identity.Name + "Appointmentlstdoctor", lstdoctor);
            }
            objModel.AvailableDoctors = lstdoctor;
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Create(PatientViewModel model)
        {
            try
            {
                //model.Status = "Booked";

                Appointment objAppoint = new Appointment();

                //var claims1 = ClaimsPrincipal.Current.Identities.First();
                objAppoint.PatientId= Int32.Parse(User.Claims.FirstOrDefault(x=>x.Type== CLAIM_TYPE_NAME).Value);                
                //objAppoint.PatientId = Int32.Parse(User.Claims.);
                    objAppoint.DoctorId = model.SelectedDoctor;
                    objAppoint.AppointmentTime = model.AppointmentDate;
                    objAppoint.Status = "Booked";
                

                _logger.LogInformation(objAppoint.PatientId.ToString());
                _logger.LogInformation(objAppoint.DoctorId.ToString());
                _logger.LogInformation(objAppoint.AppointmentTime.ToString());
                _logger.LogInformation(objAppoint.Status);

                apiProxy.PostAsync("patient/CreateAppointment", objAppoint);

                //if (ModelState.IsValid)
                //{
                //var repo = new CustomersRepository();
                //bool saved = repo.SaveCustomer(model);
                //if (saved)
                //{
                //    return RedirectToAction("Index");
                //}
                //}
                return RedirectToAction("Index","Home");

            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }
    }
}