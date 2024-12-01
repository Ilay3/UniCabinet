﻿using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel.Specialization
{
    public class SpecializationVM : SpecializationBaseVM
    {
        public int Id { get; set; }
        public List<UserVM>Teacher { get; set; }
    }
}
