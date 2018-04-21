namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SetPropertiesService</c> class.
    /// Contains methods to set values of properties in an object.
    /// <para>
    /// @author: Jennifer Nguyen, Angelica Salas Tovar
    /// @updated: 04/13/2018
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SetPropertiesService<T>
    {
        /// <summary>
        /// The SetEmptyStringToNull method.
        /// Sets the properties in an object to null if value is an empty string.
        /// <para>
        /// @author: Jennifer Nguyen, Angelica Salas Tovar
        /// @updated: 04/13/2018
        /// </para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The updated object</returns>
        public T SetEmptyStringToNull(T obj)
        {
            // Get the properties from the object and set it to PropertyInfo[] type
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // If the property value is an empty string, then set it to null
                if ((string)property.GetValue(obj) == "")
                {
                    property.SetValue(obj, null);
                }
            }

            return obj;
        }
    }
}
