using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyNow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models.Tests
{
    [TestClass()]
    public class VooTests
    {
        [TestMethod()]
        public void GerarFrequenciaTest()
        {
            List<DayOfWeek> listDias = new List<DayOfWeek>();
            listDias.Add(DayOfWeek.Monday); listDias.Add(DayOfWeek.Friday);

            Voo.GerarFrequencia(30, listDias);
        }
    }
}