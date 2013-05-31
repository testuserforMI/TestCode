using MI_UI_Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework
{
    public class FrameworkBase
    {
        private static Logger _log;
        public void SetLoggerPath(string fileName, string directory)
        {
            if (Log == null)
            {
                Log = new Logger(fileName, directory);
            }
        }

        public static Logger Log
        {
            get
            {
                return _log;
            }
            set
            {
                _log = value;
            }
        }
    }
}
