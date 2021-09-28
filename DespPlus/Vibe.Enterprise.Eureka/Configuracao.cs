using System;
using PowerDev.Enterprise.Eureka.Interfaces;
using AutoMapper;

namespace PowerDev.Enterprise.Eureka
{
    public class Configuracao
    {

        public static Func<IDisplayNameExtrator> DisplayNameExtrator { get; set; }

    }
}