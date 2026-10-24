using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Classes;
using WebApp.Api.Models;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private const string _issuer = "MyApplicationName";
        private readonly UserModel _user;

        public TwoFactorAuthenticationController(UserModel user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("user")]
        [ProducesResponseType(typeof(UserModel), 200)]
        public ActionResult GetUser()
        {
            return Ok(_user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        [HttpGet("enable")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(400)]
        public ActionResult Enable(string? inputCode = null)
        {
            var twoFactor = new TwoFactorAuthenticator();
            if (string.IsNullOrEmpty(inputCode))
            {
                var setupInfo = twoFactor.GenerateSetupCode(_issuer, _user.Email, TwoFactorKey(_user), false, 3);
                string setupCode = setupInfo.ManualEntryKey;
                string barcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                return Ok(new { barcodeImageUrl, setupCode });
            }
            else if (twoFactor.ValidateTwoFactorPIN(TwoFactorKey(_user), inputCode))
            {
                _user.TwoFactorEnabled = true;
                return Ok(_user);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        [HttpPost("disable")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(400)]
        public IActionResult Disable(string inputCode)
        {
            var twoFactor = new TwoFactorAuthenticator();
            if (twoFactor.ValidateTwoFactorPIN(TwoFactorKey(_user), inputCode))
            {
                _user.TwoFactorEnabled = false;
                return Ok(_user);
            }

            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        [HttpPost("authorizetest")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post_AuthorizeTest(string inputCode)
        {
            var twoFactor = new TwoFactorAuthenticator();
            if (twoFactor.ValidateTwoFactorPIN(TwoFactorKey(_user), inputCode))
                return Ok(_user);

            return BadRequest();
        }


        #region PRIVATE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string TwoFactorKey(UserModel user)
        {
            return $"{EnvironmentVariables.TwoFactorKey}{user.Email}";
        }

        #endregion
    }
}

/*
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <h1>Running...</h1>
    <div></div>
    <img />
    <script>
        fetch('./authentication/enable')
            .then(resp => resp.json())
            .then(d => {
                const el = document.querySelector("img");
                el.src = d.barcodeImageUrl;

                const div = document.querySelector("div");
                div.innerText = d.setupCode;
            });
    </script>
</body>
</html>
 */