﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd {
    internal interface IExporter {
        public void SaveTo(List<Employer> inputList, string outputPath);
    }
}