using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCompany.Models;

namespace prjCompany.ViewModels
{
    public class CVMDepEmp
    {
        //定義CVMDepEmp類別擁有department屬性，用來存放tDeparment的List串列物件
        public List<tDeparment> department { get; set; }

        //定義CVMDepEmp類別擁有employee屬性，用來存放tEmployee的List串列物件
        public List<tEmployee> employee { get; set; }
    }
}