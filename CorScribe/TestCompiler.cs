
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
        public static object Eval(string vbCode)
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

            string code = sb.ToString();
            sb.Clear();
            sb = null;

            System.CodeDom.Compiler.CompilerResults cr = c.CompileAssemblyFromSource(cp, code);
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
