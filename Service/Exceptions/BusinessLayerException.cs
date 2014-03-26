using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    /// <summary>
    /// Исключение бизнес-слоя
    /// </summary>
    public class BusinessLayerException : Exception
    {
        public BusinessLayerException() : base("Произошла ошибка в бизнес слое.")
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public BusinessLayerException(string message)
            : base(message)
        {

        }
    }
}
