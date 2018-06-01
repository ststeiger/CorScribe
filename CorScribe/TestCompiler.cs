
namespace CorScribe
{


    public class ReportParameter
    {
        public string Key;
        public string Label;
        public string Value;

        public ReportParameter(string key, string lab, string val)
        {
            this.Key = key;
            this.Label = lab;
            this.Value = val;
        }

        public ReportParameter(string key, string val) : this(key, val, val)
        {
        }
    }


    public class ParameterStore
    {
        private System.Collections.Generic.Dictionary<string, ReportParameter> dict;

        public ParameterStore(System.Collections.Generic.Dictionary<string, ReportParameter> parameters)
        {
            this.dict = parameters;
        }


        public ParameterStore()
            : this(new System.Collections.Generic.Dictionary<string, ReportParameter>(System.StringComparer.OrdinalIgnoreCase))
        { }


        public ParameterStore(params ReportParameter[] u) 
            : this()
        {
            for (int i = 0; i <= u.Length - 1; i += 1)
                this.dict.Add(u[i].Key, u[i]);
        }


        public void Add(ReportParameter para)
        {
            this.dict.Add(para.Key, para);
        }


        public ReportParameter this[string index]
        {
            get
            {
                return dict[index];
            }

            set
            {
                dict[index] = value;
            }
        }

    }



    /*
     
Public Class ReportParameter
    Public Key As String
    Public Label As String
    Public Value As String

    Public Sub New(key As String, lab As String, val As String)
        Me.Key = key
        Me.Label = lab
        Me.Value = val
    End Sub

    Public Sub New(key As String, val As String)
        Me.New(key, val, val)
    End Sub

End Class



Public Class ParameterStore

    Private dict As System.Collections.Generic.Dictionary(Of String, ReportParameter)


    Public Sub New(ParamArray u As ReportParameter())
        Me.New()

        For i As Integer = 0 To u.Length - 1 Step 1
            Me.dict.Add(u(i).Key, u(i))
        Next

    End Sub


    Public Sub New(parameters As System.Collections.Generic.Dictionary(Of String, ReportParameter))
        Me.dict = parameters
    End Sub


    Public Sub New()
        dict = New System.Collections.Generic.Dictionary(Of String, ReportParameter)(System.StringComparer.OrdinalIgnoreCase)
    End Sub


    Public Sub Add(para As ReportParameter)
        Me.dict.Add(para.Key, para)
    End Sub


    Default Public Property Parameters(ByVal index As String) As ReportParameter
        Get
            Return dict(index)
        End Get

        Set(ByVal value As ReportParameter)
            dict(index) = value
        End Set

    End Property

    ' Z = iif(y=0, 0, x/y)  'Throws a divide by zero exception when y is 0
    Public Shared Function IIF(expression As Boolean, truePart As Object, falsePart As Object) As Object
        If expression Then
            Return truePart
        End If

        Return falsePart
    End Function

     
    Public Shared Function SelVB() As String
        Dim Parameters As ParameterStore = New ParameterStore()

        Parameters.Add(New ReportParameter("stadtkreis", "Alle", "@stadtkreis"))
        Parameters.Add(New ReportParameter("Gemeinde", "Alle", "@Gemeinde"))


        SelVB(Parameters)
    End Function



    Public Shared Function SelVB(Parameters As ParameterStore)
        Dim str As String = "SELECT     '00000000-0000-0000-0000-000000000000' AS ID, 'Alle' AS Name, - 1 AS Sort " &
"UNION " &
"SELECT     VB_ApertureID AS ID, VB_Name AS Name, VB_Sort AS Sort " &
"FROM         V_AP_BERICHT_SEL_VB " &
"WHERE ('" & Parameters!stadtkreis.Value & "' = '00000000-0000-0000-0000-000000000000' OR VB_KS_UID = '" & Parameters!Gemeinde.Value & "') " &
"ORDER BY Sort, Name "

        Return str
    End Function

        
    Public Shared Function Gebäude(Parameters As ParameterStore) As String
        Dim str As String = "SELECT gbi.Gemeinde, gbi.Kreis, gbi.Vermessungsbezirk, gbi.Schulkreis, gbi.Standort, gbi.Gebaeude, gbi.SO_Name, gbi.GBI_Name, gbi.GBI_Adresse, gbi.GBI_Bemerkungen, gbi.GS_Lang, pf.PF1_ID, pf.PF1_Lang, pf.PF2_ID, pf.PF2_Lang, pf.PF3_ID, pf.PF3_Lang, pf.PF4_ID, pf.PF4_Lang, gbi.GS_UID,  gbi.SO_Sort, gbi.GBI_Sort, gbi.SK_Sort, gbi.GS_Sort, pf.PF1_Sort, pf.PF2_Sort, pf.PF3_Sort, pf.PF4_Sort, gbi.SK_UID AS SK_ID, gbi.GBI_IsDenkmalschutz, gbi.GBI_BemDenkmalschutz, gbi.SO_ID, gbi.GBI_ID, gbi.GM_ID, gbi.KS_ID, gbi.VB_ID, gbi.GM_Sort, gbi.KS_Sort, gbi.VB_Sort " &
"FROM V_AP_BERICHT_GBI AS gbi LEFT OUTER JOIN V_AP_BERICHT_GBI_Portfolio AS pf ON gbi.GBI_ID = pf.GBI_ID " &
"WHERE 1=1 " &
IIF(Parameters!schulkreis.Value = "00000000-0000-0000-0000-000000000000", "", "AND gbi.SK_UID='" & Parameters!schulkreis.Value & "' ") &
IIF(Parameters!stadtkreis.Value = "00000000-0000-0000-0000-000000000000", "", "AND gbi.KS_ID='" & Parameters!stadtkreis.Value & "' ") &
IIF(Parameters!schuleinheit.Value = "00000000-0000-0000-0000-000000000000", "AND 1=1 ", "AND SE_UID='" & Parameters!schuleinheit.Value & "' ") &
IIF(Parameters!portfolio1.Value = "00000000-0000-0000-0000-000000000000", "", "AND (pf.PF1_ID='" & Parameters!portfolio1.Value & "' ") &
IIF(Parameters!portfolio1.Value = "00000000-0000-0000-0000-000000000000", "", "   OR pf.PF2_ID='" & Parameters!portfolio1.Value & "') ") &
IIF(Parameters!subportfolio1.Value = "00000000-0000-0000-0000-000000000000", "", "AND (pf.PF3_ID='" & Parameters!subportfolio1.Value & "' ") &
IIF(Parameters!subportfolio1.Value = "00000000-0000-0000-0000-000000000000", "", "OR pf.PF4_ID='" & Parameters!subportfolio1.Value & "') ") &
IIF(Parameters!denkmalschutz.Value = "2", "", "AND gbi.GBI_IsDenkmalschutz=" & Parameters!denkmalschutz.Value & " ") &
IIF(Parameters!kontakt.Value = "00000000-0000-0000-0000-000000000000", "", "AND (gbi.GBI_ID IN (SELECT DPS_GBI_UID FROM dbo.T_Detail_Personen WHERE CONVERT(varchar(36), DPS_KF_UID) + CONVERT(varchar(36), DPS_KT_UID) ='" & Parameters!kontakt.Value & "' ))")



        ' ERROR: IIF(Parameters!denkmalschutz.Value = "2", "", "AND gbi.GBI_IsDenkmalschutz=" & Parameters!denkmalschutz.Value & " ") &

        Return str
    End Function


            ' https://stackoverflow.com/questions/6841275/what-does-this-mean-in-the-specific-line-of-code
    Public Shared Function ExecuteParameter(Parameters As ParameterStore) As String
        Dim str As String = "SELECT 2 AS ID, 2 AS Sort, 'Alle' AS Name " &
"UNION " &
"SELECT DISTINCT PZ_IsAltlasten AS ID, PZ_IsAltlasten AS Sort, 'Ja' AS Name " &
"FROM V_AP_BERICHT_SEL_PZ_Altlasten  " &
"WHERE PZ_IsAltlasten = 1  " &
IIF(Parameters!Vermessungsbezirk.Value = "00000000-0000-0000-0000-000000000000", IIF(Parameters!Kreis.Value = "00000000-0000-0000-0000-000000000000", IIF(Parameters!Gemeinde.Value = "00000000-0000-0000-0000-000000000000", "", "AND GM_ApertureID = '" & Parameters!Gemeinde.Value & "' "), "AND KS_ApertureID = '" & Parameters!Kreis.Value & "' "), "AND VB_ApertureID = '" & Parameters!Vermessungsbezirk.Value & "' ") &
"UNION " &
"SELECT DISTINCT PZ_IsAltlasten AS ID, PZ_IsAltlasten AS Sort, 'Nein' AS Name " &
"FROM V_AP_BERICHT_SEL_PZ_Altlasten " &
"WHERE PZ_IsAltlasten = 0 " &
IIF(Parameters!Vermessungsbezirk.Value = "00000000-0000-0000-0000-000000000000", IIF(Parameters!Kreis.Value = "00000000-0000-0000-0000-000000000000", IIF(Parameters!Gemeinde.Value = "00000000-0000-0000-0000-000000000000", "", "AND GM_ApertureID = '" & Parameters!Gemeinde.Value & "' "), "AND KS_ApertureID = '" & Parameters!Kreis.Value & "' "), "AND VB_ApertureID = '" & Parameters!Vermessungsbezirk.Value & "' ") &
"ORDER BY Sort DESC "

        ' str.Trim(New Char() {" "c, vbTab, vbCr, vbLf})

        Return str
    End Function
    
End Class

    */


    public class TestCompiler
    {
        // Z = iif(y=0, 0, x/y)  'Throws a divide by zero exception when y is 0
        public static object IIF(bool expression, object truePart, object falsePart)
        {
            if (expression)
                return truePart;

            return falsePart;
        }

        public static string ExecuteParameter()
        {
            ParameterStore Parameters = new ParameterStore();
            // https://msdn.microsoft.com/en-us/library/microsoft.visualbasic.controlchars.cr(v=vs.110).aspx
            // Microsoft.VisualBasic.ControlChars  My.InternalXmlHelper


            Parameters.Add(new ReportParameter("Vermessungsbezirk", "Alle", "00000000-0000-0000-0000-000000000000"));
            Parameters.Add(new ReportParameter("Kreis", "Alle", "00000000-0000-0000-0000-000000000000"));
            Parameters.Add(new ReportParameter("Gemeinde", "Alle", "00000000-0000-0000-0000-000000000000"));

            string str = ""; // ExecuteParameter(Parameters);

            System.Console.WriteLine(str);
            return str;
        }



        // http://stackoverflow.com/questions/2684278/why-does-microsoft-jscript-work-in-the-code-behind-but-not-within-a
        protected static object Eval(string vbCode)
        {
            // MsgBox(vbCode)
            Microsoft.VisualBasic.VBCodeProvider c = new Microsoft.VisualBasic.VBCodeProvider();
            // Dim icc As System.CodeDom.Compiler.ICodeCompiler = c.CreateCompiler()
            System.CodeDom.Compiler.CompilerParameters cp = new System.CodeDom.Compiler.CompilerParameters();

            cp.ReferencedAssemblies.Add("System.dll");
            // cp.ReferencedAssemblies.Add("System.Xml.dll")
            // cp.ReferencedAssemblies.Add("System.Data.dll")
            cp.ReferencedAssemblies.Add("Microsoft.JScript.dll");

            // Sample code for adding your own referenced assemblies
            // cp.ReferencedAssemblies.Add("c:\yourProjectDir\bin\YourBaseClass.dll")
            // cp.ReferencedAssemblies.Add("YourBaseclass.dll")


            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("Imports System" + Microsoft.VisualBasic.Constants.vbCrLf);
            // sb.Append("Imports System.Xml" & vbCrLf)
            // sb.Append("Imports System.Data" & vbCrLf)
            // sb.Append("Imports System.Data.SqlClient" & vbCrLf)
            // sb.Append("Imports System.Math" & vbCrLf)
            sb.Append("Imports Microsoft.VisualBasic" + Microsoft.VisualBasic.Constants.vbCrLf);

            sb.Append("Namespace MyEvalNamespace_2C04AF0B_8EC8_4D84_AD83_51FFBBBBC8C6BFAC7762_5502_40C9_AE35_A545434012A4528B58FB_641E_4D61_BA15_E64C58CD8CFC  " + Microsoft.VisualBasic.Constants.vbCrLf);
            sb.Append("Class MyEvalClass " + Microsoft.VisualBasic.Constants.vbCrLf);

            sb.Append("Public Function EvalCode() As Object " + Microsoft.VisualBasic.Constants.vbCrLf);
            // sb.Append("YourNamespace.YourBaseClass thisObject = New YourNamespace.YourBaseClass()")
            sb.Append(vbCode + Microsoft.VisualBasic.Constants.vbCrLf);
            sb.Append("End Function " + Microsoft.VisualBasic.Constants.vbCrLf);
            sb.Append("End Class " + Microsoft.VisualBasic.Constants.vbCrLf);
            sb.Append("End Namespace " + Microsoft.VisualBasic.Constants.vbCrLf);
            // Debug.WriteLine(sb.ToString()) ' look at this to debug your eval string

            // Dim cr As System.CodeDom.Compiler.CompilerResults = icc.CompileAssemblyFromSource(cp, sb.ToString())
            System.CodeDom.Compiler.CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o;
            System.Reflection.MethodInfo mi;
            o = a.CreateInstance("MyEvalNamespace_2C04AF0B_8EC8_4D84_AD83_51FFBBBBC8C6BFAC7762_5502_40C9_AE35_A545434012A4528B58FB_641E_4D61_BA15_E64C58CD8CFC.MyEvalClass");
            System.Type t = o.GetType();
            mi = t.GetMethod("EvalCode");
            object s;
            s = mi.Invoke(o, null);
            return s;
        }


    }


}
