using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Reflection;

namespace TruckMove.API.BLL.Services
{


    public class ObjectUpdater<T1, T2>
    {
        public T2 Map(T1 source, T2 destination)
        {
            Type sourceType = typeof(T1);
            Type destinationType = typeof(T2);

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] destinationProperties = destinationType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                // Find corresponding property in destination type
                PropertyInfo destinationProperty = Array.Find(destinationProperties, prop => prop.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    //if (fieldValue != null && fieldValue.GetType() == correspondingProperty.PropertyType)
                    // Check if the types match before setting value
                    if (sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        // Get the value from the source object
                        object value = sourceProperty.GetValue(source);

                        // Set the value to the corresponding property in the destination object
                        if (value != null)
                        {
                            destinationProperty.SetValue(destination, value);
                        }
                       
                    }
                }


            }
            return destination;

        }
    }
}
