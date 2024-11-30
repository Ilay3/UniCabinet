﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Core.Models.ViewModel.Specialization;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
    public class SpecAndDepVM
    {
        public List<DepartmantVM> DepartmetVM {  get; set; }

        public List<SpecializationVM> SpecializationVM { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }
    }
}