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
using Autodesk.AutoCAD.Windows.Data;

namespace TreinamentoPluginCAD
{
    public class Main
    {
        #region INPUTS DO USUÁRIO
        [CommandMethod("PEGARPONTO")]
        public void PegarPonto()
        {
            
            PromptPointOptions optionPoint = new PromptPointOptions("\nEspecifique o ponto no desenho");

            // Solicita a selecão de um ponto no desenho
            PromptPointResult pointClicked = Manager.docEditor.GetPoint(optionPoint);

            // Verifica se deu certo
            if (pointClicked.Status == PromptStatus.OK)
            {
                // Escreve uma mensagem
                Manager.docEditor.WriteMessage($"O valor X:{pointClicked.Value.X.ToString()} | O Valor de Y: {pointClicked.Value.Y}");
            }
        }

        [CommandMethod("PEGARNUMERO")]
        public void PegarNumero()
        {
            PromptDoubleOptions optionDouble = new PromptDoubleOptions("\nDigite um número");
            optionDouble.DefaultValue = 6;

            PromptDoubleResult numberInput = Manager.docEditor.GetDouble(optionDouble);
            if (numberInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O valor digitado é: {numberInput.Value}", "O valor foi digitado :)", MessageBoxButtons.OK);
            }
        }

        [CommandMethod("PEGARTEXTO")]
        public void PegarTexto()
        {
            PromptStringOptions optionString = new PromptStringOptions("\nDigite uma palavra");
            optionString.AllowSpaces = true;

            PromptResult txtInput = Manager.docEditor.GetString(optionString);
            if (txtInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O texto digitado foi: {txtInput.StringResult}");
            }
        }

        [CommandMethod("PEGARINTEIRO")]
        public void PegarInteiro()
        {
            PromptIntegerOptions optionInteger = new PromptIntegerOptions("\nEspecifique o número inteiro");
            optionInteger.DefaultValue = 2;

            PromptIntegerResult numInput = Manager.docEditor.GetInteger(optionInteger);
            if(numInput.Status == PromptStatus.OK)
            {
                MessageBox.Show($"O número digitado foi: {numInput.Value}");
            }
        }

        [CommandMethod("PEGARENTIDADE")]
        public void PegarEntidade()
        {
            PromptEntityOptions optionEntity = new PromptEntityOptions("\nSelecione a polyline");
            optionEntity.SetRejectMessage("\nSelecione apenas polylines!");
            optionEntity.AddAllowedClass(typeof(Polyline), true);

            PromptEntityResult resultSel = Manager.docEditor.GetEntity(optionEntity);
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
            PromptPointResult pointClicked = Manager.docEditor.GetPoint("\nEspecifique o ponto inicial");
            if (pointClicked.Status == PromptStatus.OK)
            {
                PromptCornerOptions optionCorner = new PromptCornerOptions("Especifique o segundo ponto", pointClicked.Value);
                optionCorner.UseDashedLine = true;

                PromptPointResult pointCorner = Manager.docEditor.GetCorner(optionCorner);
                if (pointCorner.Status == PromptStatus.OK)
                {
                    MessageBox.Show($"Primeira Coordenada: {pointClicked.Value.X}, {pointClicked.Value.Y}");
                    MessageBox.Show($"Segunda Coordenada: {pointCorner.Value.X}, {pointCorner.Value.Y}");
                }
            }
        }
        [CommandMethod("SELECIONARCIRCULOS")]
        public void SelecionaCirculos()
        {
            TypedValue type = new TypedValue(0, "CIRCLE");
            SelectionFilter selFilter = new SelectionFilter(new TypedValue[] { type });
            PromptSelectionOptions optionSelection = new PromptSelectionOptions();
            optionSelection.MessageForAdding = "\nSelecione os círculos";
            optionSelection.MessageForRemoval = "\nApenas círculos";

            PromptSelectionResult selObjects = Manager.docEditor.GetSelection(optionSelection, selFilter);

            if (selObjects.Status == PromptStatus.OK)
            {
                PromptDoubleResult radius = Manager.docEditor.GetDouble("\nEspecifique o raio: ");

                if (radius.Status == PromptStatus.OK)
                {
                    Autodesk.AutoCAD.Windows.ColorDialog colorWin = new Autodesk.AutoCAD.Windows.ColorDialog();

                    if (colorWin.ShowDialog() == DialogResult.OK)
                    {
                        using (Transaction trans = Manager.docData.TransactionManager.StartTransaction())
                        {
                            for (int i = 0; i < selObjects.Value.Count; i++)
                            {
                                Circle circle = (Circle)trans.GetObject(selObjects.Value[i].ObjectId, OpenMode.ForWrite);
                                circle.Color = colorWin.Color;
                                circle.Radius = radius.Value;
                            }
                            trans.Commit();
                        }
                    }
                }
            }
        }
        [CommandMethod("AREACOMPRIMENTO")]
        public void AreaComprimento()
        {
            TypedValue type = new TypedValue(0, "LWPOLYLINE");
            SelectionFilter selFilter = new SelectionFilter(new TypedValue[] { type });
            PromptSelectionOptions optionSelection = new PromptSelectionOptions();
            optionSelection.MessageForAdding = "\nSelecione as polilinhas";
            optionSelection.MessageForRemoval = "\nApenas polilinhas";

            PromptSelectionResult selObjects = Manager.docEditor.GetSelection(optionSelection, selFilter);
            if (selObjects.Status == PromptStatus.OK)
            {
                using (Transaction trans = Manager.docData.TransactionManager.StartTransaction())
                {
                    double totalArea = 0;
                    double totalLength = 0; 

                    for (int i = 0; i < selObjects.Value.Count; i++)
                    {
                        Polyline polyL = (Polyline)trans.GetObject(selObjects.Value[i].ObjectId, OpenMode.ForRead);
                        if (polyL.Closed == true)
                        {
                            totalArea += polyL.Area;
                        }
                        totalLength += polyL.Length;
                    }

                    MessageBox.Show($"A área total: {totalArea.ToString("N3")} | O Comprimento total: {totalLength.ToString("N3")}");
                    trans.Commit();
                }
            }

        }

        #endregion

        #region CRIAÇÃO DE OBJETOS

        [CommandMethod("CRIAPONTO")]
        public void CriaPonto()
        {
            PromptPointResult pointClicked = Manager.docEditor.GetPoint("\nEspecifique o ponto");
            if (pointClicked.Status == PromptStatus.OK)
            {
                using (Transaction trans = Manager.docData.TransactionManager.StartTransaction())
                {
                    BlockTableRecord model = (BlockTableRecord)trans.GetObject(Manager.docData.CurrentSpaceId, OpenMode.ForWrite);

                    DBPoint point = new DBPoint(pointClicked.Value);
                    point.ColorIndex = 4;
                    point.Layer = "0";

                    model.AppendEntity(point);
                    trans.AddNewlyCreatedDBObject(point, true);
                    trans.Commit();
                }
            }
        }

        [CommandMethod("CRIALINHA")]
        public void CriaLinha()
        {
            PromptPointResult point1 = Manager.docEditor.GetPoint("\nEspecifique o ponto inicial");
            if (point1.Status == PromptStatus.OK)
            {
                PromptPointOptions optionPoint = new PromptPointOptions("\nEspecifique o ponto final");
                optionPoint.UseBasePoint = true;
                optionPoint.BasePoint = point1.Value;
                optionPoint.UseDashedLine = true;

                PromptPointResult point2 = Manager.docEditor.GetPoint(optionPoint);
                if (point2.Status == PromptStatus.OK)
                {
                    using (Transaction trans = Manager.docData.TransactionManager.StartTransaction())
                    {
                        BlockTableRecord model = (BlockTableRecord)trans.GetObject(Manager.docData.CurrentSpaceId, OpenMode.ForWrite);
                        Line line = new Line(point1.Value, point2.Value);
                        line.ColorIndex = 10;

                        model.AppendEntity(line);
                        trans.AddNewlyCreatedDBObject(line, true);
                        trans.Commit();
                    }
                }
            }
        }

        [CommandMethod("CRIAPOLYLINE")]
        public void CriaPolyline()
        {
            List<Point3d> listPoint = new List<Point3d>();

            VOLTAR:
            PromptPointResult point = Manager.docEditor.GetPoint("\nEspecifique o ponto");
            if(point.Status == PromptStatus.OK)
            {
                listPoint.Add(point.Value);
                goto VOLTAR;
            }
            else
            {
                if (listPoint.Count > 1)
                {
                    using (Transaction trans = Manager.docData.TransactionManager.StartTransaction())
                    {
                        BlockTableRecord model = (BlockTableRecord)trans.GetObject(Manager.docData.CurrentSpaceId, OpenMode.ForWrite);

                        Polyline polyL = new Polyline();

                        for (int i = 0; i < listPoint.Count; i++)
                        {
                            polyL.AddVertexAt(i, listPoint[i].Convert2d(new Plane()), 0, 0, 0);
                        }

                        model.AppendEntity(polyL);
                        trans.AddNewlyCreatedDBObject(polyL, true);
                        trans.Commit();
                    }
                }
            }
        }

        #endregion
    }
}
