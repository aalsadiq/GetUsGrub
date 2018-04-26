using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub
{
    /// <summary>
    /// ResetPassword controller will handle routes that deal with resetting a password for a user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/25/2018
    /// </para>
    /// </summary>
    [RoutePrefix("ResetPassword")]
    public class ResetPasswordController : ApiController
    {
        /// <summary>
        /// The GetSecurityQuestions method.
        /// Validates model state and routes the data transfer object to get Security Questions.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("GetSecurityQuestions")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult GetSecurityQuestions([FromBody] ResetPasswordDto resetPasswordDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var resetPasswordManager = new ResetPasswordManager(resetPasswordDto);
                var response = resetPasswordManager.GetSecurityQuestions();
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                // Sending HTTP response 200 Status
                return Ok(response.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// The ConfirmSecurityQuestionAnswers method.
        /// Validates model state and routes the data transfer object to confirm security answers.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("ConfirmSecurityAnswers")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult ConfirmSecurityQuestionAnswers([FromBody] ResetPasswordDto resetPasswordDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var resetPasswordManager = new ResetPasswordManager(resetPasswordDto);
                var response = resetPasswordManager.ConfirmSecurityQuestionAnswers();
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                // Sending HTTP response 200 Status
                return Ok();
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// The UpdatePassword method.
        /// Validates model state and routes the data transfer object to update user's password.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("UpdatePassword")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult UpdatePassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var resetPasswordManager = new ResetPasswordManager(resetPasswordDto);
                var response = resetPasswordManager.UpdatePassword();
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                // Sending HTTP response 200 Status
                return Ok();
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}