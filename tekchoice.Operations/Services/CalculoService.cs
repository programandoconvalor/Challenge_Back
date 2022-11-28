using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using tekchoice.Data.DataContext;
using tekchoice.Data.Models;
using tekchoice.Operations.Interfaces;

namespace tekchoice.Operations.Services
{
    public class CalculoService : ICalculo
    {
        public DataContext _calculoDbContext;
        public CalculoService(DataContext calculoDbContext)
        {
            _calculoDbContext = calculoDbContext;
        }
        public CalculoModel Create(CalculoModel val)
        {
            float altura = 0;
            float grasa = 0;
            // reemplazar punto
            if (val.altura.ToString().Contains("."))
            {
                altura = float.Parse(val.altura) * 100;
            }
            else
            {
                altura = float.Parse(val.altura);
            }
            // diferenciar genero y aplicar algoritmo de calculo de grasa coorporal
            if (val.sexo == "H")
            {
                grasa = 495 / (float)(1.0324 - 0.19077 * (Math.Log10(float.Parse(val.cintura) - float.Parse(val.cuello))) + 0.15456 * (Math.Log10(altura))) - 450;
            }
            else
            {
                grasa = 495 / (float)(1.29579 - 0.35004 * (Math.Log10((float.Parse(val.cadera) + float.Parse(val.cintura)) - float.Parse(val.cuello))) + 0.22100 * (Math.Log10(float.Parse(val.altura)))) - 450;
            }
            // convertir resultado con 2 decimales
            var resulultado = Math.Truncate(grasa * 100) / 100;
            // convertir resultado a entero para aplicar regla de negocio
            int regla = Convert.ToInt32(resulultado);
            // Caso Esencial 2-4
            if (regla < 4)
            {
                val.sexo = regla.ToString() +"-" + resulultado.ToString();
            }
            // Caso Deportista  6-13

            else if (regla < 13)
            {
                val.sexo = regla.ToString() + "-" + resulultado.ToString();
            }
            // Caso Fitness 14-17

            else if (regla < 17)

            {
                val.sexo = regla.ToString() + "-" + resulultado.ToString();
            }
            // Caso Aceptable 18-25
            else if (regla < 25)
            {
                val.sexo = regla.ToString() + "-" + resulultado.ToString();
            }
            // Caso Obeso 25 +
            else
            {
                val.sexo = regla.ToString() + "-" + resulultado.ToString();
            }
            return val;
        }
    }
}
