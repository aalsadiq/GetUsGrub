using CSULB.GetUsGrub.Models;
using System;
using System.IO;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Image service that will user profile or menu item images.
    /// </summary>
    public class ImageService:IDisposable
    {
        /// <summary>
        /// Delete Image, deletes the image when given a path of the image.
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 04/26/2018
        /// </para>
        /// </summary>
        /// <param name="path">The physical path of image.</param>
        /// <param name="image">The virtual path of image. Is used to get the file name.</param>
        /// <returns></returns>
        public ResponseDto<bool> DeleteImage(string path) //, string image
        {
            try
            {
                // Check if path exists
                if (File.Exists(path))
                {
                    // Delete Image if path exists
                    File.Delete(path);

                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = ValidationErrorMessages.IMAGE_DOES_NOT_EXIST
                };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
