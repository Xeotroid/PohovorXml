using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd {
    public class Deserialiser {
        private Config _config;

        public Deserialiser(Config config) {
            _config = config;
            if (config.InputPaths.Count == 0 || config.OutputPath == string.Empty) {
                throw new InvalidDataException();
            }
        }

        public bool Work() {
            return true;
        }
    }
}
