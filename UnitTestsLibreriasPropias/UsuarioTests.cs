using Microsoft.VisualStudio.TestTools.UnitTesting;
using admUsuarios.Entidad;
namespace UnitTests.LibreriasPropias
{
    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void Test_HacerLoginUsuario_ReturnUsuario()
        {
            //arrange
            var userlogin = "usrsistemas";
            var password = "123456";    

            //act
            var result = Usuario.HacerLogIN(userlogin, password);
            //assert
            Assert.IsNotNull(result);
        }
    }
}
