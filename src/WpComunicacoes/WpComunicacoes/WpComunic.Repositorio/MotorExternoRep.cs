using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpComunic.Infra.Infraestrutura;
using WpComunicacoes.Entidades;

namespace WpComunic.Repositorio
{
    public class MotorExternoRep : Base<MotorExterno>
    {
        public MotorExterno GetMotorExterno(int id)
        {
            using (var context = new WpContext())
            {
                var motor = context.motorExternos
                    .Include(motor => motor.metodo).ThenInclude(metodo => metodo.ClasseEntrada).ThenInclude(classe => classe.propriedades)
                    .Include(motor => motor.metodo).ThenInclude(metodo => metodo.ClasseSaida).ThenInclude(classe => classe.propriedades)
                    .ToList().Where(x => x.ID == id).FirstOrDefault();
                return motor;

            }
        }
    }
}
