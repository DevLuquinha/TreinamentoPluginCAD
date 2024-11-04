using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System.Windows.Forms;

namespace TreinamentoPluginCAD
{
    public class Main
    {
        [CommandMethod("PEGARPONTO")]
        public void PegarPonto()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptPointOptions optionPoint = new PromptPointOptions("\nEspecifique o ponto no desenho");

            // Solicita a selecão de um ponto no desenho
            PromptPointResult pointClicked = docEditor.GetPoint(optionPoint);

            // Verifica se deu certo
            if (pointClicked.Status == PromptStatus.OK)
            {
                // Escreve uma mensagem
                docEditor.WriteMessage($"O valor X:{pointClicked.Value.X.ToString()} | O Valor de Y: {pointClicked.Value.Y}");
            }
        }

        [CommandMethod("PEGARNUMERO")]
        public void PegarNumero()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptDoubleOptions optionDouble = new PromptDoubleOptions("\nDigite um número");
            optionDouble.DefaultValue = 6;

            PromptDoubleResult numberInput = docEditor.GetDouble(optionDouble);
            if (numberInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O valor digitado é: {numberInput.Value}", "O valor foi digitado :)", MessageBoxButtons.OK);
            }
        }

        [CommandMethod("PEGARTEXTO")]
        public void PegarTexto()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptStringOptions optionString = new PromptStringOptions("\nDigite uma palavra");
            optionString.AllowSpaces = true;

            PromptResult txtInput = docEditor.GetString(optionString);
            if (txtInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O texto digitado foi: {txtInput.StringResult}");
            }
        }

        [CommandMethod("PEGARINTEIRO")]
        public void PegarInteiro()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptIntegerOptions optionInteger = new PromptIntegerOptions("\nEspecifique o número inteiro");
            optionInteger.DefaultValue = 2;

            PromptIntegerResult numInput = docEditor.GetInteger(optionInteger);
            if(numInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O número digitado foi: {numInput.Value}");
            }
        }

        [CommandMethod("PEGARENTIDADE")]
        public void PegarEntidade()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptEntityOptions optionEntity = new PromptEntityOptions("\nSelecione a polyline");
            optionEntity.SetRejectMessage("\nSelecione apenas polylines!");
            optionEntity.AddAllowedClass(typeof(Polyline), true);

            PromptEntityResult resultSel = docEditor.GetEntity(optionEntity);
            if (resultSel.Status == PromptStatus.OK)
            {
                MessageBox.Show("O Objeto FOI selecionado", "Sucesso :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("O Objeto não foi selecionado", "Erro!", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }

        [CommandMethod("PEGARCORNER")]
        public void PegarCorner()
        {
            // Recebe as principais variaveis da autodesk
            Document docCAD = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database docData = docCAD.Database;
            Editor docEditor = docCAD.Editor;

            PromptPointResult pointClicked = docEditor.GetPoint("\nEspecifique o ponto inicial");
            if (pointClicked.Status == PromptStatus.OK)
            {
                PromptCornerOptions optionCorner = new PromptCornerOptions("Especifique o segundo ponto", pointClicked.Value);
                optionCorner.UseDashedLine = true;

                PromptPointResult pointCorner = docEditor.GetCorner(optionCorner);
                if (pointCorner.Status == PromptStatus.OK)
                {
                    MessageBox.Show($"Primeira Coordenada: {pointClicked.Value.X}, {pointClicked.Value.Y}");
                    MessageBox.Show($"Segunda Coordenada: {pointCorner.Value.X}, {pointCorner.Value.Y}");
                }
            }
        }
    }
}
