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

            // Solicita a selecão de um ponto no desenho
            PromptPointResult pointClicked = docEditor.GetPoint("Selecione um ponto");

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

            PromptDoubleResult numberInput = docEditor.GetDouble("Especifique o comprimento: ");
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

            PromptResult txtInput = docEditor.GetString("Digite uma palavra");
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

            PromptIntegerResult numInput = docEditor.GetInteger("Especifique o número inteiro");
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

            PromptEntityResult resultSel = docEditor.GetEntity("Selecione o Objeto");
            if (resultSel.Status == PromptStatus.OK)
            {
                MessageBox.Show("O Objeto FOI selecionado", "Sucesso :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("O Objeto não foi selecionado", "Erro!", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }
    }
}
