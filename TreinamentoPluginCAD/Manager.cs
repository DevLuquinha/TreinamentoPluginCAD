using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoPluginCAD
{
    public static class Manager
    {
        public static Document docCAD  { get { return Application.DocumentManager.MdiActiveDocument; } }
        public static Database docData { get { return Application.DocumentManager.MdiActiveDocument.Database; } }
        public static Editor docEditor { get { return Application.DocumentManager.MdiActiveDocument.Editor; } }
    }
}
