using System.IdentityModel.Tokens.Jwt;
using Charpter.WebApi.Controllers;
using Charpter.WebApi.interfaces;
using Charpter.WebApi.Models;
using Charpter.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TesteXUNIT.Controllers;

public class LoginControllerTeste
{
    [Fact]
    public void LoginController_DeveRetornar_UsuarioInvalido()
    {
        //arrange
        var fakeRepository = new Mock<IUsuarioRepository>();
        fakeRepository.Setup(x => x.Login(It.IsAny<string>(),It.IsAny<string>())).Returns((Usuario)null);

        LoginViewModel dadosLogin = new LoginViewModel();
        dadosLogin.Email = "email@email.com";
        dadosLogin.Senha = "1234";

        var controller = new LoginController(fakeRepository.Object);
        
        //act

        var resultado = controller.Login(dadosLogin);

        //Assert
        
        Assert.IsType<UnauthorizedObjectResult>(resultado);    
    }

    [Fact]
    public void LoginController_DeveRetornar_Token()
    {
        Usuario usuarioRetorno = new Usuario();
        usuarioRetorno.Email = "email@email.com";
        usuarioRetorno.Senha = "1234";

        var fakeRepository = new Mock<IUsuarioRepository>();
        fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);

        string issuerValidacao = "chapter.webapi";

        LoginViewModel dadosLogin = new LoginViewModel();
        dadosLogin.Email = "email@email.com";
        dadosLogin.Senha = "1234";

        var controller = new LoginController(fakeRepository.Object);
        
        // act
        OkObjectResult resultado = (OkObjectResult) controller.Login(dadosLogin);

        string token = resultado.Value.ToString().Split(' ')[3];

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenJwt = jwtHandler.ReadJwtToken(token);
        
        // assert
        
        Assert.Equal(issuerValidacao, tokenJwt.Issuer);
    }
}