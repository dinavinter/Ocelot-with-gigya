using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Ocelot.Web.Pages.OP;

namespace Gigya.Identity.Client.Controllers
{
    public class OPController : Controller
    {
        
        // [ActionName("proxy")]
        // public IActionResult OPProxy()
        // {
        //     return View();
        // }
        //  
        // [ActionName("login")]
        // public IActionResult OPLogin()
        // {
        //     return View();
        // }
        //
        // [ActionName("error")]
        // public IActionResult OPErrorPage()
        // {
        //     return View();
        // }
        private readonly IActionContextAccessor _actionContext;

        public OPController(IActionContextAccessor actionContext)
        {
            _actionContext = actionContext;
        }

        public static string CalcSignature(string text, string key)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(new HMACSHA1(Convert.FromBase64String(key)).ComputeHash(bytes));
        }

        [ActionName("consent")]
        public IActionResult Consent(
            string context,
            string clientID,
            string scope,
            string prompt,
            string display,
            string UID,
            string UIDSignature,
            string signatureTimestamp
        )
        {
            scope = scope?.Replace("+", " ").Replace("%2b", " ");

            string userKey= "AIvzv0Pv8IFX";
            string userSecret= "e6DgUArTi5mGQgaqqAzPdFNYiWaPNaqu";
             
            var redirectUrl = $"proxy?mode=afterConsent&consent={HttpUtility.UrlEncode(consentObjString())}&sig={consentObjSig()}&userKey={userKey}&context={context}";
            return Redirect(redirectUrl);
         
            // return View("proxy",   new
            // {
            //     domain = RouteData.Domain(),
            //     apikey = RouteData.ApiKey(), 
            //     mode = "afterConsent",
            //     consent = HttpUtility.UrlEncode(consentObjString()),
            //     userKey = userKey,
            //     context = context,
            //     sig = consentObjSig()
            // });
            // return View(new {consent= consentObjString(), consentSignature= consentObjSig(), userKey= userKey});
            string consentObjString()
            {
                return JsonConvert.SerializeObject(new
                {
                    scope = HttpUtility.UrlDecode(scope),
                    clientID = clientID,
                    context = context,
                    UID = UID,
                    consent = true
                });
                

            }
            
            string consentObjSig()
            {
               return  CalcSignature(consentObjString(), userSecret).Trim('=').Replace("+", "-").Replace("/", "_");
            }
            
            
        }

         
    }
}