//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Manga.Models;
using MangaExpressController.Areas.Identity.Pages.AdminP.MangasP;
using Xunit;

namespace UnitTestingXUnitIntro.Tests
{
    public class TestShould
    {

        //public MangaM manga;
       
        
       

        

        [Fact]
        public void ValidatorUpload()
        {

            
           
            var createModal = new CreateModel();
            int Precio = 150;
            string Nombre ="Inuyasha";
            string Descripcion="Manga de fantasia y yokai";
            //Act
            bool isValid = createModal.ValidateInputs(Precio, Nombre, Descripcion);

            //Assert
            Assert.True(isValid);
        }
    }
}
