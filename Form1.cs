using System.IO.Enumeration;

namespace SanitizeFilenames
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IEnumerable<string> files;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            files = Directory.EnumerateFiles(Directory.GetParent(openFileDialog1.FileName).FullName);

            fillDatagrid();
        }
        private void fillDatagrid()
        {
            dataGridView1.Rows.Clear();

            int i = 0;
            foreach (var file in files)
            {
                string filename = new FileInfo(file).Name;
                string newname = CleanFilename(filename);

                // if name is clean already, ignore it in the table
                if (newname == "")
                    continue;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = file;
                dataGridView1.Rows[i].Cells[2].Value = newname;
                i++;
            }
        }
        private string CleanFilename(string filename)
        {
            List<char> chars = new List<char>();
            string newfilename = "";
            bool badFilename = false;

            // bad filename chars for windows : \ / : * ? " < > | 
           
            List<char> triggeredCharacters = new List<char>();

            // if (filename.Contains("Froggin"))
            //     ;

            foreach (char b in filename)
            {
                if (isValidChar((short)b))
                    chars.Add(b);
                else
                {
                    triggeredCharacters.Add(b);
                    badFilename = true;
                }
            }

            foreach (var b in chars)
                newfilename = $"{newfilename}{b}";

            // return blank incase the filename was clean already
            if (badFilename)
                return newfilename;

            return "";
        }
        private bool isValidChar(short i)
        {
            switch (i)
            {
                case 0x20: return true; // space
                case 0x21: return true; // 	!	Exclamation mark ; 
                case 0x22: return false; // &quot;	"	Double quote             ; ok
                case 0x23: return true; // 	#	Number                       ; ok
                case 0x24: return true; // 	$	Dollar                       ; ok
                case 0x25: return true; // 	%	Percent                      ; ok
                case 0x26: return true; // &amp;	&	Ampersand                ; ok
                case 0x27: return true; // 	'	Single quote                 ; ok
                case 0x28: return true; // 	(	Left parenthesis             ; ok
                case 0x29: return true; // 	)	Right parenthesis            ; ok
                case 0x2A: return false; // 	*	Asterisk                     ; 
                case 0x2B: return true; // 	+	Plus                         ; ok
                case 0x2C: return true; // 	,	Comma                        ; ok
                case 0x2D: return true; // 	-	Minus                        ; ok
                case 0x2E: return true; // 	.	Period                       ; ok
                case 0x2F: return false; // 	/	Slash                        ; 
                case 0x30: return true; // 	0	Zero                         ; ok
                case 0x31: return true; // 	1	One                          ; ok
                case 0x32: return true; // 	2	Two                          ; ok
                case 0x33: return true; // 	3	Three                        ; ok
                case 0x34: return true; // 	4	Four                         ; ok
                case 0x35: return true; // 	5	Five                         ; ok
                case 0x36: return true; // 	6	Six                          ; ok
                case 0x37: return true; // 	7	Seven                        ; ok
                case 0x38: return true; // 	8	Eight                        ; ok
                case 0x39: return true; // 	9	Nine                         ; ok
                case 0x3A: return false; // 	:	Colon                        ; ok
                case 0x3B: return true; // 	;	Semicolon                    ; ok
                case 0x3C: return false; // &lt;	<	Less than                ; 
                case 0x3D: return true; // 	=	Equal sign                   ; 
                case 0x3E: return false; // &gt;	>	Greater than             ; 
                case 0x3F: return false; // 	?	Question mark                ; 
                case 0x40: return true; // 	@	At sign                      ; 
                case 0x41: return true; // 	A	Uppercase A                  ; ok
                case 0x42: return true; // 	B	Uppercase B                  ; ok
                case 0x43: return true; // 	C	Uppercase C                  ; ok
                case 0x44: return true; // 	D	Uppercase D                  ; ok
                case 0x45: return true; // 	E	Uppercase E                  ; ok
                case 0x46: return true; // 	F	Uppercase F                  ; ok
                case 0x47: return true; // 	G	Uppercase G                  ; ok
                case 0x48: return true; // 	H	Uppercase H                  ; ok
                case 0x49: return true; // 	I	Uppercase I                  ; ok
                case 0x4A: return true; // 	J	Uppercase J                  ; ok
                case 0x4B: return true; // 	K	Uppercase K                  ; ok
                case 0x4C: return true; // 	L	Uppercase L                  ; ok
                case 0x4D: return true; // 	M	Uppercase M                  ; ok
                case 0x4E: return true; // 	N	Uppercase N                  ; ok
                case 0x4F: return true; // 	O	Uppercase O                  ; ok
                case 0x50: return true; // 	P	Uppercase P                  ; ok
                case 0x51: return true; // 	Q	Uppercase Q                  ; ok
                case 0x52: return true; // 	R	Uppercase R                  ; ok
                case 0x53: return true; // 	S	Uppercase S                  ; ok
                case 0x54: return true; // 	T	Uppercase T                  ; ok
                case 0x55: return true; // 	U	Uppercase U                  ; ok
                case 0x56: return true; // 	V	Uppercase V                  ; ok
                case 0x57: return true; // 	W	Uppercase W                  ; ok
                case 0x58: return true; // 	X	Uppercase X                  ; ok
                case 0x59: return true; // 	Y	Uppercase Y                  ; ok
                case 0x5A: return true; // 	Z	Uppercase Z                  ; ok
                case 0x5B: return true; // 	[	Left square bracket          ; ok
                case 0x5C: return false; // 	\	backslash                    ; 
                case 0x5D: return true; // 	]	Right square bracket         ; ok
                case 0x5E: return true; // 	^	Caret / circumflex           ; 
                case 0x5F: return true; // 	_	Underscore                   ; ok
                case 0x60: return false; // 	`	Grave / accent               ; 
                case 0x61: return true; // 	a	Lowercase a                  ; ok
                case 0x62: return true; // 	b	Lowercase b                  ; ok
                case 0x63: return true; // 	c	Lowercase c                  ; ok
                case 0x64: return true; // 	d	Lowercase d                  ; ok
                case 0x65: return true; // 	e	Lowercase e                  ; ok
                case 0x66: return true; // 	f	Lowercase                    ; ok
                case 0x67: return true; // 	g	Lowercase g                  ; ok
                case 0x68: return true; // 	h	Lowercase h                  ; ok
                case 0x69: return true; // 	i	Lowercase i                  ; ok
                case 0x6A: return true; // 	j	Lowercase j                  ; ok
                case 0x6B: return true; // 	k	Lowercase k                  ; ok
                case 0x6C: return true; // 	l	Lowercase l                  ; ok
                case 0x6D: return true; // 	m	Lowercase m                  ; ok
                case 0x6E: return true; // 	n	Lowercase n                  ; ok
                case 0x6F: return true; // 	o	Lowercase o                  ; ok
                case 0x70: return true; // 	p	Lowercase p                  ; ok
                case 0x71: return true; // 	q	Lowercase q                  ; ok
                case 0x72: return true; // 	r	Lowercase r                  ; ok
                case 0x73: return true; // 	s	Lowercase s                  ; ok
                case 0x74: return true; // 	t	Lowercase t                  ; ok
                case 0x75: return true; // 	u	Lowercase u                  ; ok
                case 0x76: return true; // 	v	Lowercase v                  ; ok
                case 0x77: return true; // 	w	Lowercase w                  ; ok
                case 0x78: return true; // 	x	Lowercase x                  ; ok
                case 0x79: return true; // 	y	Lowercase y                  ; ok
                case 0x7A: return true; // 	z	Lowercase z                  ; ok
                case 0x7B: return true; // 	{	Left curly bracket           ; ok
                case 0x7C: return false; // 	\		Vertical bar             ; 
                case 0x7D: return true; // 	}	Right curly bracket          ; ok
                case 0x7E: return false; // 	~	Tilde                        ; 

                // case 0xfb01: return "fi"; // ﬁ fuck periphery

                default:
                    Console.WriteLine($"Unhandeled filename char: {i}");
                    return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var row in dataGridView1.Rows)
            {
                if ((bool)((DataGridViewRow)row).Cells[0].FormattedValue == false)
                {
                    string oldname = (string)((DataGridViewRow)row).Cells[1].Value;
                    string newname = (string)((DataGridViewRow)row).Cells[2].Value;

                    renameFile(oldname, newname);
                    toolStripStatusLabel1.Text = $"Renamed {oldname} to: {newname}";
                }
            }
        }

        private void renameFile(string filename, string newfilename)
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(filename, newfilename);
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("Could not complete operation since a file already exists in this path"))
                    return;

                toolStripStatusLabel1.Text = e.Message;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            items1 = new List<string>();

            string path = folderBrowserDialog1.SelectedPath;

            EnumerateItems(path, items1, 0);

            files = items1;

            foreach (string file in items1)
            {
                files.Append(file);
            }
            

            fillDatagrid();
        }
        public static List<string> items1 = new List<string>();
        private static void EnumerateItems(string path, List<string> items, int val)
        {
            if (path == null)
                return;

            if (path == "")
                return;

            foreach (var f in Directory.EnumerateFiles(path))
            {
                items.Add(f);
            }

            foreach (var f in Directory.EnumerateDirectories(path))
                EnumerateItems(f, items, val);

        }
    }
}
