﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Diplom.DOMAIN.NewModels
{
    public partial class Videos
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }

        public virtual Persons Person { get; set; }
    }
}