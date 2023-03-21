using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi_.Data;
using UsuariosApi_.Data.Request;
using UsuariosApi_.Models;

namespace UsuariosApi_.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroService(IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public async Task<Result> CadastraUsuario(CreateUsuarioDto_ createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            var resultadoEmail = _userManager.Users.FirstOrDefault(x => x.Email == usuario.Email);
            

            if (resultadoEmail != null)
            {
                return Result.Fail("Este e-mail já está cadastrado.");
            }

            IdentityResult resultadoIdentity = await _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            await _userManager.AddToRoleAsync(usuarioIdentity, createDto.Role);

            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager.
                    GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email },
                    "Link de Ativação", usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess("Usuário cadastrado com sucesso: " + usuarioIdentity.UserName + ". Por gentileza verifique seu e-mail para validar a conta.");
            }
            return Result.Fail("Falha no cadastro do usuário!");

        }

        public async Task<Result> AtivaContausuario(AtivaContaRequest request)
        {
            var identityUser = _userManager.Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);

            var IdentityResult = await _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao);

            if (IdentityResult.Succeeded)
            {
                return Result.Ok().WithSuccess("Conta ativada com sucesso");
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}