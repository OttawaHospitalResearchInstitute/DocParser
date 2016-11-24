using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocParser
{
    public class Ref
    {
        public msSQLclass mySQLclass = new msSQLclass();
        public static String DBirisPrefix = "iris.dbo.";
        public static String DBProtrac = "Protrac.dbo.";

        //Testing DB        
        //public static String PeoplePrefix = "People2014_04232014.dbo.";
        //Live DB        
        public static String PeoplePrefix = "People2014.dbo.";
    }
}