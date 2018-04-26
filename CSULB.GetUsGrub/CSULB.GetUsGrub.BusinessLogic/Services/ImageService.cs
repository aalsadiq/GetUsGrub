using CSULB.GetUsGrub.Models;
using System;
using System.IO;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Image service that will user profile or menu item images.
    /// </summary>
    public class ImageService
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
        public ResponseDto<bool> deleteImage(string path, string image)
        {
            try
            {
                var imageName = Path.GetFileName(image);
                var filePath = path + imageName;

                // Check if path exists
                if (File.Exists(filePath))
                {
                    // Delete Image if path exists
                    File.Delete(filePath);

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
    }
}
