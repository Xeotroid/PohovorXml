using System.Runtime.CompilerServices;

namespace BackEnd {
    public class LogHelper {
        public static log4net.ILog GetLogger([CallerFilePath]string filename = "") {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}
