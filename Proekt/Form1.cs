using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Proekt
{
    public partial class Form1 : Form
    {
        const int cellSize = 110;
        int[,] map = new int[8, 8];
        Image white;
        Image black;
        Image whiteking;
        Image blackking;
        Button PreviousButton;

        PictureBox picturebox1 = new PictureBox();

        int whosemove;
        bool inthehands;
        bool nowissecondeat;
        Button[,] fieldcells = new Button[8, 8];

        public Form1()
        {
            InitializeComponent();
            white = new Bitmap(new Bitmap("../../../data/notking1.jpg"), new Size(cellSize - 7, cellSize - 7));
            black = new Bitmap(new Bitmap("../../../data/notking2.jpg"), new Size(cellSize, cellSize));
            whiteking = new Bitmap(new Bitmap("../../../data/king1.jpg"), new Size(cellSize - 7, cellSize - 7));
            blackking = new Bitmap(new Bitmap("../../../data/king2.jpg"), new Size(cellSize, cellSize));
            this.Text = "пивные шашки";
            Init();
            adddenis();
            TextBox textboxq = new TextBox();
            textboxq.Location = new Point(1020, cellSize * 2 + 14);
            textboxq.Size = new Size(cellSize, cellSize);
            textboxq.Text = "чей ход";
            textboxq.Enabled = false;
            this.Controls.Add(textboxq);
        }
        public void Init()
        {
            whosemove = 2;
            inthehands = false;
            nowissecondeat = false;
            PreviousButton = null;
            picturebox1.Location = new Point(1020, cellSize * 2 + 42);
            picturebox1.Size = new Size(cellSize, cellSize);
            picturebox1.Image = black;
            this.Controls.Add(picturebox1);
            map = new int[8, 8] {
                                    { 0,1,0,1,0,1,0,1 },
                                    { 1,0,1,0,1,0,1,0 },
                                    { 0,1,0,1,0,1,0,1 },
                                    { 0,0,0,0,0,0,0,0 },
                                    { 0,0,0,0,0,0,0,0 },
                                    { 2,0,2,0,2,0,2,0 },
                                    { 0,2,0,2,0,2,0,2 },
                                    { 2,0,2,0,2,0,2,0 }
                                };
            
            CreateMap();

        }
        public void CreateMap()
        {

            this.Width = 8 * cellSize + 20 + 70 + 170;
            this.Height = 8 * cellSize + 50 + 20;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(i * cellSize, j * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Click += new EventHandler(OnFigurePress);
                    if (map[j, i] == 1)
                    {
                        button.Image = white;
                        if (nowthekiïg(button))
                            button.Image = whiteking;

                    }
                    if (map[j, i] == 2)
                    {
                        button.Image = black;
                        if (nowthekiïg(button))
                            button.Image = blackking;
                    }
                    if (i % 2 == j % 2)
                    {
                        button.BackColor = Color.BlanchedAlmond;
                    }

                    fieldcells[j, i] = button;

                    this.Controls.Add(button);

                }
            }

        }
        public void SwitchPlayer()
        {
            if (whosemove == 1) whosemove = 2;
            else whosemove = 1;

            picturebox1.Location = new Point(1020, cellSize * 2 + 42);
            picturebox1.Size = new Size(cellSize, cellSize);
            if (whosemove == 1)
                picturebox1.Image = white;
            else
                picturebox1.Image = black;
            this.Controls.Add(picturebox1);

            end();
        }
        public void OnFigurePress(object sender, EventArgs e)
        {
            backtowhite();
            Button pressedButton = sender as Button;

            if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == whosemove)
            {
                showmytern(pressedButton);
                inthehands = true;
                PreviousButton = pressedButton;
            }
            else
            {
                if (inthehands) //åñëè õîäèì ñâîåé øàøêîé
                {
                    if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)//åñëè õîäèì â ïóñòóþ êëåòêó
                    {
                        onButtons();
                        if (nowthekiïg(PreviousButton) == false)// õîäèì îáû÷íîé øàøêîé
                        {
                            if (//åñëè õîäèò ïåðâûé èãðîê 
                                    whosemove == 1 && pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize + 1
                                    & (pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize - 1
                                    || pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize + 1)
                               )
                            {
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = temp;
                                if (nowthekiïg(pressedButton))
                                    pressedButton.Image = whiteking;
                                else
                                    pressedButton.Image = PreviousButton.Image;
                                PreviousButton.Image = null;
                                inthehands = false;
                                nowissecondeat = false;
                                SwitchPlayer();

                            }

                            if (//åñëè åñò ïåðâûé èãðîê
                                   whosemove == 1 && (pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize + 2
                                   || pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize - 2)
                                   & (pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize - 2
                                   || pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize + 2)
                                   & map[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2] == 2
                                )
                            {
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = temp;
                                if (nowthekiïg(pressedButton))
                                    pressedButton.Image = whiteking;
                                else
                                    pressedButton.Image = PreviousButton.Image;
                                map[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2] = 0;
                                fieldcells[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2].Image = null;
                                PreviousButton.Image = null;
                                inthehands = false;
                                if ((!nowthekiïg(pressedButton) & caneatnotking(pressedButton)) & (nowthekiïg(pressedButton) & !caneatking(pressedButton)))
                                {
                                    SwitchPlayer();
                                    nowissecondeat = false;
                                }
                                else // ïîâòîðíîå âçÿòèå
                                {
                                    offButtons();
                                    if (pressedButton.Image == white)
                                        onButtons_secondeat_notking(pressedButton);
                                    if (pressedButton.Image == whiteking)
                                    {
                                        onButtons_secondeat_king(pressedButton, napr(pressedButton, PreviousButton));
                                    }
                                    pressedButton.Enabled = true;
                                    nowissecondeat = true;
                                    if (proverka() == 1)
                                    {
                                        onButtons();
                                        nowissecondeat = false;
                                        SwitchPlayer();
                                    }
                                }
                            }

                            if (//åñëè õîäèò âòîðîé èãðîê 
                                whosemove == 2 && pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize - 1
                                & (pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize - 1
                                || pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize + 1)
                               )
                            {
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = temp;
                                if (nowthekiïg(pressedButton))
                                    pressedButton.Image = blackking;
                                else
                                    pressedButton.Image = PreviousButton.Image;
                                PreviousButton.Image = null;
                                inthehands = false;
                                nowissecondeat = false;
                                SwitchPlayer();
                            }
                            if (//åñëè åñò âòîðîé èãðîê
                                   whosemove == 2 && (pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize + 2
                                   || pressedButton.Location.Y / cellSize == PreviousButton.Location.Y / cellSize - 2)
                                   & (pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize - 2
                                   || pressedButton.Location.X / cellSize == PreviousButton.Location.X / cellSize + 2)
                                   & map[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2] == 1
                               )
                            {
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = temp;
                                if (nowthekiïg(pressedButton))
                                    pressedButton.Image = blackking;
                                else
                                    pressedButton.Image = PreviousButton.Image;
                                map[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2] = 0;
                                fieldcells[(pressedButton.Location.Y / cellSize + PreviousButton.Location.Y / cellSize) / 2, (pressedButton.Location.X / cellSize + PreviousButton.Location.X / cellSize) / 2].Image = null;
                                PreviousButton.Image = null;
                                inthehands = false;
                                if ((!nowthekiïg(pressedButton) & caneatnotking(pressedButton)) & (nowthekiïg(pressedButton) & !caneatking(pressedButton)))
                                {
                                    SwitchPlayer();
                                    nowissecondeat = false;
                                }
                                else // ïîâòîðíîå âçÿòèå
                                {
                                    offButtons();
                                    if (pressedButton.Image == black)
                                        onButtons_secondeat_notking(pressedButton);
                                    if (pressedButton.Image == blackking)
                                        onButtons_secondeat_king(pressedButton, napr(pressedButton, PreviousButton));
                                    pressedButton.Enabled = true;
                                    nowissecondeat = true;
                                    if (proverka() == 1)
                                    {
                                        onButtons();
                                        nowissecondeat = false;
                                        SwitchPlayer();
                                    }
                                }
                            }
                        }
                        if (nowthekiïg(PreviousButton))
                        {
                            if (howmuchbetween(PreviousButton, pressedButton) == 0  // õîäèì äàìêîé, íî íå åäèì
                                & (pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize
                                || pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == -(pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize)))
                            {
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = temp;
                                pressedButton.Image = PreviousButton.Image;
                                PreviousButton.Image = null;
                                inthehands = false;
                                nowissecondeat = false;
                                SwitchPlayer();
                            }
                            if (howmuchbetween(PreviousButton, pressedButton) == 1 & !candoubleeatking(PreviousButton, pressedButton)// åäèì äàìêîé 1 ðàç
                                & (pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize
                                || pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == -(pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize)))
                            {
                                //fieldcells[0, 0].BackColor = Color.Khaki; //ïðîâåðî÷êà, ÷òî ìû çàøëè â ýòîò èô
                                int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = 0;
                                pressedButton.Image = PreviousButton.Image;
                                whobetween(PreviousButton, pressedButton).Image = null;
                                map[whobetween(PreviousButton, pressedButton).Location.Y / cellSize, whobetween(PreviousButton, pressedButton).Location.X / cellSize] = 0;
                                PreviousButton.Image = null;
                                inthehands = false;
                                SwitchPlayer();
                                nowissecondeat = false;
                                if (proverka() == 1)
                                {
                                    onButtons();
                                    nowissecondeat = false;
                                    SwitchPlayer();
                                }
                            }

                            if (howmuchbetween(PreviousButton, pressedButton) == 1 & candoubleeatking(PreviousButton, pressedButton)// åäèì äàìêîé >1 ðàçà
                                & (pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize
                                || pressedButton.Location.Y / cellSize - PreviousButton.Location.Y / cellSize == -(pressedButton.Location.X / cellSize - PreviousButton.Location.X / cellSize)))
                            {
                                if (isdoubleeat(PreviousButton, pressedButton))
                                {
                                    //fieldcells[0, 0].BackColor = Color.Purple; //ïðîâåðî÷êà, ÷òî ìû çàøëè â ýòîò èô
                                    int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                                    map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize];
                                    map[PreviousButton.Location.Y / cellSize, PreviousButton.Location.X / cellSize] = 0;
                                    pressedButton.Image = PreviousButton.Image;
                                    whobetween(PreviousButton, pressedButton).Image = null;
                                    map[whobetween(PreviousButton, pressedButton).Location.Y / cellSize, whobetween(PreviousButton, pressedButton).Location.X / cellSize] = 0;
                                    PreviousButton.Image = null;
                                    inthehands = false;

                                    offButtons();
                                    onButtons_secondeat_king(pressedButton, napr(PreviousButton, pressedButton));
                                    pressedButton.Enabled = true;
                                    nowissecondeat = true;
                                    PreviousButton = pressedButton;
                                }
                            }
                        }
                    }
                }

            }
            if (nowissecondeat == false) // ïðîâåðêà íà âîçìîæíûå ïîåäàíèÿ
            {
                needtoeat();
            }
        }
        public void showmytern(Button button)
        {
            if (button.Image == white)
            {
                if (caneatnotking(button))
                {
                    if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                            fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                            fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                            fieldcells[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                            fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2].BackColor = Color.Green;
                }
                else
                {
                    if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 0)
                            fieldcells[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 0)
                            fieldcells[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1].BackColor = Color.Green;
                }
            }



            if (button.Image == black)
            {
                if (caneatnotking(button))
                {
                    if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                            fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                        if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                            fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                            fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                            fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2].BackColor = Color.Green;
                }
                else
                {
                    if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 0)
                            fieldcells[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1].BackColor = Color.Green;

                    if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1))
                        if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 0)
                            fieldcells[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1].BackColor = Color.Green;
                }
            }
            if (button.Image == whiteking || button.Image == blackking)
            {
                if (caneatking(button))
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            if (fieldcells[i, j].Enabled == true)
                                fieldcells[i, j].BackColor = Color.Green;
                }
                else
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (onthefield(button.Location.Y / cellSize - i, button.Location.X / cellSize - i))
                        {
                            if (map[button.Location.Y / cellSize - i, button.Location.X / cellSize - i] != 0)
                                break;
                            else
                                fieldcells[button.Location.Y / cellSize - i, button.Location.X / cellSize - i].BackColor = Color.Green;
                        }
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        if (onthefield(button.Location.Y / cellSize - i, button.Location.X / cellSize + i))
                        {
                            if (map[button.Location.Y / cellSize - i, button.Location.X / cellSize + i] != 0)
                                break;
                            else
                                fieldcells[button.Location.Y / cellSize - i, button.Location.X / cellSize + i].BackColor = Color.Green;
                        }
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        if (onthefield(button.Location.Y / cellSize + i, button.Location.X / cellSize - i))
                        {
                            if (map[button.Location.Y / cellSize + i, button.Location.X / cellSize - i] != 0)
                                break;
                            else
                                fieldcells[button.Location.Y / cellSize + i, button.Location.X / cellSize - i].BackColor = Color.Green;
                        }
                    }
                    for (int i = 1; i < 8; i++)
                    {
                        if (onthefield(button.Location.Y / cellSize + i, button.Location.X / cellSize + i))
                        {
                            if (map[button.Location.Y / cellSize + i, button.Location.X / cellSize + i] != 0)
                                break;
                            else
                                fieldcells[button.Location.Y / cellSize + i, button.Location.X / cellSize + i].BackColor = Color.Green;
                        }
                    }
                }
            }
        }
        public int howmuchbetween(Button bilo, Button stalo)
        {
            int n = Math.Abs(bilo.Location.Y / cellSize - stalo.Location.Y / cellSize);
            int zy = Math.Sign(stalo.Location.Y / cellSize - bilo.Location.Y / cellSize);
            int zx = Math.Sign(stalo.Location.X / cellSize - bilo.Location.X / cellSize);
            int count = 0;
            for (int i = 1; i < n; i++)
            {
                if (map[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx] == map[bilo.Location.Y / cellSize, bilo.Location.X / cellSize])
                    return -1;
                if (map[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx] != map[bilo.Location.Y / cellSize, bilo.Location.X / cellSize]
                    && map[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx] != 0)
                    count++;
            }
            return count;
        }
        public Button whobetween(Button bilo, Button stalo)
        {
            int n = Math.Abs(bilo.Location.Y / cellSize - stalo.Location.Y / cellSize);
            int zy = Math.Sign(stalo.Location.Y / cellSize - bilo.Location.Y / cellSize);
            int zx = Math.Sign(stalo.Location.X / cellSize - bilo.Location.X / cellSize);
            for (int i = 1; i < n; i++)
            {
                if (map[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx] != map[bilo.Location.Y / cellSize, bilo.Location.X / cellSize]
                    && map[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx] != 0)
                {
                    return fieldcells[bilo.Location.Y / cellSize + i * zy, bilo.Location.X / cellSize + i * zx];
                }
            }
            return null;
        }
        public bool onthefield(int x, int y)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
                return true;
            else
                return false;
        }
        public bool icanturn(Button button)
        {
            if (button.Image == white)
            {
                if (caneatnotking(button))
                    return true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 0)
                        return true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 0)
                        return true;
            }

            if (button.Image == black)
            {
                if (caneatnotking(button))
                    return true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 0)
                        return true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 0)
                        return true;
            }

            if (button.Image == blackking || button.Image == whiteking)
            {
                if (caneatking(button))
                    return true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 0)
                        return true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 0)
                        return true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 0)
                        return true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 0)
                        return true;
            }

            return false;
        }
        public void end()
        {
            bool white = false;
            bool black = false;
            bool draw = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] == 1)
                        white = true;
                    if (map[i, j] == 2)
                        black = true;
                }
            }
            if (white & black)
            {
                draw = true;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (map[i, j] == whosemove)
                        {
                            if (icanturn(fieldcells[i, j]))
                                draw = false;
                        }
                    }
                }
            }
            if (!black)
            {
                this.Controls.Clear();
                Button button = new Button();
                button.Location = new Point(200, 100);
                button.Size = new Size(500, 200);
                button.Text = "сегодня пьем балтику 9!";
                this.Controls.Add(button);
            }
            if (!white)
            {
                this.Controls.Clear();
                Button button = new Button();
                button.Location = new Point(200, 100);
                button.Size = new Size(500, 200);
                button.Text = "сегодня пьем жигулевское!";
                this.Controls.Add(button);
            }
            if (draw & whosemove == 2)
            {
                this.Controls.Clear();
                Button button = new Button();
                button.Location = new Point(200, 100);
                button.Size = new Size(500, 200);
                button.Text = "сегодня пьем балтику 9!";
                this.Controls.Add(button);
            }

            if (draw & whosemove == 1)
            {
                this.Controls.Clear();
                Button button = new Button();
                button.Location = new Point(200, 100);
                button.Size = new Size(500, 200);
                button.Text = "сегодня пьем жигулевское!";
                this.Controls.Add(button);
            }
        }
        public bool nowthekiïg(Button button)
        {
            if ((button.Location.Y / cellSize == 0 && map[button.Location.Y / cellSize, button.Location.X / cellSize] == 2)
              || (button.Location.Y / cellSize == 7 && map[button.Location.Y / cellSize, button.Location.X / cellSize] == 1)
              || button.Image == whiteking || button.Image == blackking)
            {
                return true;
            }
            else
                return false;
        }
        public bool caneatnotking(Button button)
        {
            if (map[button.Location.Y / cellSize, button.Location.X / cellSize] == 2)
            {
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                        return true;
            }
            if (map[button.Location.Y / cellSize, button.Location.X / cellSize] == 1)
            {
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                        return true;
                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                        return true;
            }
            return false;
        }
        public bool caneatking(Button button)
        {
            for (int q = 2; q < 8; q++)
                if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize + q))
                {
                    if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q]) == 1
                        & map[button.Location.Y / cellSize + q, button.Location.X / cellSize + q] == 0)
                    {
                        return true;
                    }
                }

            for (int q = 2; q < 8; q++)
                if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize - q))
                {
                    if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q]) == 1
                        & map[button.Location.Y / cellSize + q, button.Location.X / cellSize - q] == 0)
                    {
                        return true;
                    }
                }

            for (int q = 2; q < 8; q++)
                if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize + q))
                {
                    if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q]) == 1
                        & map[button.Location.Y / cellSize - q, button.Location.X / cellSize + q] == 0)
                    {
                        return true;
                    }
                }

            for (int q = 2; q < 8; q++)
                if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize - q))
                {
                    if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q]) == 1
                        & map[button.Location.Y / cellSize - q, button.Location.X / cellSize - q] == 0)
                    {
                        return true;
                    }
                }

            return false;
        }
        public void onButtons_secondeat_notking(Button button)
        {
            if (map[button.Location.Y / cellSize, button.Location.X / cellSize] == 2)
            {
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                        fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                        fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                        fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 1 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                        fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2].Enabled = true;
            }
            if (map[button.Location.Y / cellSize, button.Location.X / cellSize] == 1)
            {
                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2] == 0)
                        fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize + 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2] == 0)
                        fieldcells[button.Location.Y / cellSize + 2, button.Location.X / cellSize - 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2] == 0)
                        fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize - 2].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2))
                    if (map[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1] == 2 && map[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2] == 0)
                        fieldcells[button.Location.Y / cellSize - 2, button.Location.X / cellSize + 2].Enabled = true;
            }
        }
        public void onButtons_secondeat_king(Button button, int napr)
        {
            
                for (int q = 2; q < 8; q++)
                    if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize + q))
                        if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q]) == 1
                            & map[button.Location.Y / cellSize + q, button.Location.X / cellSize + q] == 0)
                            fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q].Enabled = true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1))
                    if (candoubleeatking(button, fieldcells[button.Location.Y / cellSize + 1, button.Location.X / cellSize + 1]))
                    {
                        for (int q = 2; q < 8; q++)
                            if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize + q))
                                if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q]) == 1
                                    & map[button.Location.Y / cellSize + q, button.Location.X / cellSize + q] == 0)
                                    if (!isdoubleeat(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q]))
                                        fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize + q].Enabled = false;
                    }
            
            
                for (int q = 2; q < 8; q++)
                    if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize - q))
                        if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q]) == 1
                            & map[button.Location.Y / cellSize + q, button.Location.X / cellSize - q] == 0)
                            fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q].Enabled = true;

                if (onthefield(button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1))
                    if (candoubleeatking(button, fieldcells[button.Location.Y / cellSize + 1, button.Location.X / cellSize - 1]))
                    {
                        for (int q = 2; q < 8; q++)
                            if (onthefield(button.Location.Y / cellSize + q, button.Location.X / cellSize - q))
                                if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q]) == 1
                                    & map[button.Location.Y / cellSize + q, button.Location.X / cellSize - q] == 0)
                                    if (!isdoubleeat(button, fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q]))
                                        fieldcells[button.Location.Y / cellSize + q, button.Location.X / cellSize - q].Enabled = false;
                    }
            
            
                for (int q = 2; q < 8; q++)
                    if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize + q))
                        if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q]) == 1
                            & map[button.Location.Y / cellSize - q, button.Location.X / cellSize + q] == 0)
                            fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1))
                    if (candoubleeatking(button, fieldcells[button.Location.Y / cellSize - 1, button.Location.X / cellSize + 1]))
                    {
                        for (int q = 2; q < 8; q++)
                            if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize + q))
                                if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q]) == 1
                                    & map[button.Location.Y / cellSize - q, button.Location.X / cellSize + q] == 0)
                                    if (!isdoubleeat(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q]))
                                        fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize + q].Enabled = false;
                    }
            
            
                for (int q = 2; q < 8; q++)
                    if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize - q))
                        if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q]) == 1
                            & map[button.Location.Y / cellSize - q, button.Location.X / cellSize - q] == 0)
                            fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q].Enabled = true;

                if (onthefield(button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1))
                    if (candoubleeatking(button, fieldcells[button.Location.Y / cellSize - 1, button.Location.X / cellSize - 1]))
                    {
                        for (int q = 2; q < 8; q++)
                            if (onthefield(button.Location.Y / cellSize - q, button.Location.X / cellSize - q))
                                if (howmuchbetween(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q]) == 1
                                    & map[button.Location.Y / cellSize - q, button.Location.X / cellSize - q] == 0)
                                    if (!isdoubleeat(button, fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q]))
                                        fieldcells[button.Location.Y / cellSize - q, button.Location.X / cellSize - q].Enabled = false;
                    }
            
        }
        public void needtoeat()
        {
            int q = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] == whosemove)
                    {
                        if ((caneatking(fieldcells[i, j]) & (fieldcells[i, j].Image == whiteking || fieldcells[i, j].Image == blackking))
                            || (caneatnotking(fieldcells[i, j]) & (fieldcells[i, j].Image == white || fieldcells[i, j].Image == black)))
                        {
                            q++;
                        }
                    }
                }
            }
            if (q != 0)
            {
                offButtons();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (map[i, j] == whosemove)
                        {
                            if ((caneatking(fieldcells[i, j]) & (fieldcells[i, j].Image == whiteking || fieldcells[i, j].Image == blackking))
                                || (caneatnotking(fieldcells[i, j]) & (fieldcells[i, j].Image == white || fieldcells[i, j].Image == black)))
                            {
                                fieldcells[i, j].Enabled = true;
                                if (fieldcells[i, j].Image == whiteking || fieldcells[i, j].Image == blackking)
                                    onButtons_secondeat_king(fieldcells[i, j], 0);
                                if (fieldcells[i, j].Image == white || fieldcells[i, j].Image == black)
                                    onButtons_secondeat_notking(fieldcells[i, j]);
                            }
                        }
                    }
                }

            }
        }
        public void onButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    fieldcells[i, j].Enabled = true;
                }
            }
        }


        public void offButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    fieldcells[i, j].Enabled = false;
                }
            }
        }
        public void adddenis()
        {
            TextBox textbox1 = new TextBox();
            textbox1.Location = new Point(900, 42);
            textbox1.Size = new Size(cellSize, cellSize);
            textbox1.Text = "8";
            textbox1.Enabled = false;
            this.Controls.Add(textbox1);

            TextBox textbox2 = new TextBox();
            textbox2.Location = new Point(900, cellSize + 42);
            textbox2.Size = new Size(cellSize, cellSize);
            textbox2.Text = "7";
            textbox2.Enabled = false;
            this.Controls.Add(textbox2);

            TextBox textbox3 = new TextBox();
            textbox3.Location = new Point(900, cellSize * 2 + 42);
            textbox3.Size = new Size(cellSize, cellSize);
            textbox3.Text = "6";
            textbox3.Enabled = false;
            this.Controls.Add(textbox3);

            TextBox textbox4 = new TextBox();
            textbox4.Location = new Point(900, cellSize * 3 + 42);
            textbox4.Size = new Size(cellSize, cellSize);
            textbox4.Text = "5";
            textbox4.Enabled = false;
            this.Controls.Add(textbox4);

            TextBox textbox5 = new TextBox();
            textbox5.Location = new Point(900, cellSize * 4 + 42);
            textbox5.Size = new Size(cellSize, cellSize);
            textbox5.Text = "4";
            textbox5.Enabled = false;
            this.Controls.Add(textbox5);

            TextBox textbox6 = new TextBox();
            textbox6.Location = new Point(900, cellSize * 5 + 42);
            textbox6.Size = new Size(cellSize, cellSize);
            textbox6.Text = "3";
            textbox6.Enabled = false;
            this.Controls.Add(textbox6);

            TextBox textbox7 = new TextBox();
            textbox7.Location = new Point(900, cellSize * 6 + 42);
            textbox7.Size = new Size(cellSize, cellSize);
            textbox7.Text = "2";
            textbox7.Enabled = false;
            this.Controls.Add(textbox7);

            TextBox textbox8 = new TextBox();
            textbox8.Location = new Point(900, cellSize * 7 + 42);
            textbox8.Size = new Size(cellSize, cellSize);
            textbox8.Text = "1";
            textbox8.Enabled = false;
            this.Controls.Add(textbox8);


            TextBox textboxa = new TextBox();
            textboxa.Location = new Point(0 + 42, 900 - 20);
            textboxa.Size = new Size(cellSize, cellSize);
            textboxa.Text = "A";
            textboxa.Enabled = false;
            this.Controls.Add(textboxa);

            TextBox textboxb = new TextBox();
            textboxb.Location = new Point(cellSize + 42, 900 - 20);
            textboxb.Size = new Size(cellSize, cellSize);
            textboxb.Text = "B";
            textboxb.Enabled = false;
            this.Controls.Add(textboxb);

            TextBox textboxc = new TextBox();
            textboxc.Location = new Point(cellSize * 2 + 42, 900 - 20);
            textboxc.Size = new Size(cellSize, cellSize);
            textboxc.Text = "C";
            textboxc.Enabled = false;
            this.Controls.Add(textboxc);

            TextBox textboxd = new TextBox();
            textboxd.Location = new Point(cellSize * 3 + 42, 900 - 20);
            textboxd.Size = new Size(cellSize, cellSize);
            textboxd.Text = "D";
            textboxd.Enabled = false;
            this.Controls.Add(textboxd);

            TextBox textboxe = new TextBox();
            textboxe.Location = new Point(cellSize * 4 + 42, 900 - 20);
            textboxe.Size = new Size(cellSize, cellSize);
            textboxe.Text = "E";
            textboxe.Enabled = false;
            this.Controls.Add(textboxe);

            TextBox textboxf = new TextBox();
            textboxf.Location = new Point(cellSize * 5 + 42, 900 - 20);
            textboxf.Size = new Size(cellSize, cellSize);
            textboxf.Text = "F";
            textboxf.Enabled = false;
            this.Controls.Add(textboxf);

            TextBox textboxg = new TextBox();
            textboxg.Location = new Point(cellSize * 6 + 42, 900 - 20);
            textboxg.Size = new Size(cellSize, cellSize);
            textboxg.Text = "G";
            textboxg.Enabled = false;
            this.Controls.Add(textboxg);

            TextBox textboxh = new TextBox();
            textboxh.Location = new Point(cellSize * 7 + 42, 900 - 20);
            textboxh.Size = new Size(cellSize, cellSize);
            textboxh.Text = "H";
            textboxh.Enabled = false;
            this.Controls.Add(textboxh);
        }
        public void backtowhite()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 != j % 2)
                        fieldcells[i, j].BackColor = Color.White;
                }
            }
        }

        public bool candoubleeatking(Button bilo, Button stalo)
        {
            int zy = Math.Sign(stalo.Location.Y / cellSize - bilo.Location.Y / cellSize);
            int zx = Math.Sign(stalo.Location.X / cellSize - bilo.Location.X / cellSize);

            for (int q = 2; q < 8; q++)
                if (onthefield(bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx))
                {
                    if (howmuchbetween(bilo, fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx]) == 1
                        & map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] == 0)
                    {

                        map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] = map[bilo.Location.Y / cellSize, bilo.Location.X / cellSize];
                        //fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx].Text = $"{map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx]}";
                        for (int w = 2; w < 8; w++)
                        {
                            if (onthefield(bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx - w * zx))
                            {
                                //fieldcells[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx - w * zx].Text = $"{howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx - w * zx])}";
                                if (howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx - w * zx]) == 1
                                   & map[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx - w * zx] == 0)
                                {
                                    map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] = 0;
                                    return true;
                                }
                            }
                            if (onthefield(bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx))
                            {
                                //fieldcells[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx].Text = $"{howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx])}";
                                if (howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx]) == 1
                                   & map[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx] == 0)
                                {
                                    map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] = 0;
                                    return true;
                                }
                            }
                            if (onthefield(bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx + w * zx))
                            {
                                //fieldcells[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx].Text = $"{howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy - w * zy, bilo.Location.X / cellSize + q * zx + w * zx])}";
                                if (howmuchbetween(fieldcells[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx], fieldcells[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx + w * zx]) == 1
                                   & map[bilo.Location.Y / cellSize + q * zy + w * zy, bilo.Location.X / cellSize + q * zx + w * zx] == 0)
                                {
                                    map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] = 0;
                                    return true;
                                }
                            }
                        }
                        map[bilo.Location.Y / cellSize + q * zy, bilo.Location.X / cellSize + q * zx] = 0;
                    }
                }
            return false;
        }
        public int napr(Button bilo, Button stalo)
        {
            int zy = Math.Sign(stalo.Location.Y / cellSize - bilo.Location.Y / cellSize);//   1   2
            int zx = Math.Sign(stalo.Location.X / cellSize - bilo.Location.X / cellSize);//  -2  -1
            if (zy == 1 & zx == 1)
                return -1;
            if (zy == 1 & zx == -1)
                return -2;
            if (zy == -1 & zx == 1)
                return 2;
            if (zy == -1 & zx == -1)
                return 1;
            return 0;
        }
        public int proverka()
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (fieldcells[i, j].Enabled == true)
                        count++;
                }
            }
            return count;
        }
        public bool isdoubleeat(Button bilo, Button stalo)
        {
            int zy = Math.Sign(stalo.Location.Y / cellSize - bilo.Location.Y / cellSize);
            int zx = Math.Sign(stalo.Location.X / cellSize - bilo.Location.X / cellSize);

            for (int q = 2; q < 8; q++)
            {
                map[stalo.Location.Y / cellSize, stalo.Location.X / cellSize] = map[bilo.Location.Y / cellSize, bilo.Location.X / cellSize];
                if (onthefield(stalo.Location.Y / cellSize - q * zy, stalo.Location.X / cellSize + q * zx))
                    if (howmuchbetween(stalo, fieldcells[stalo.Location.Y / cellSize - q * zy, stalo.Location.X / cellSize + q * zx]) == 1
                               & map[stalo.Location.Y / cellSize - q * zy, stalo.Location.X / cellSize + q * zx] == 0)
                    {
                        map[stalo.Location.Y / cellSize, stalo.Location.X / cellSize] = 0;
                        return true;
                    }
                if (onthefield(stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize - q * zx))
                    if (howmuchbetween(stalo, fieldcells[stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize - q * zx]) == 1
                               & map[stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize - q * zx] == 0)
                    {
                        map[stalo.Location.Y / cellSize, stalo.Location.X / cellSize] = 0;
                        return true;
                    }
                if (onthefield(stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize + q * zx))
                    if (howmuchbetween(stalo, fieldcells[stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize + q * zx]) == 1
                               & map[stalo.Location.Y / cellSize + q * zy, stalo.Location.X / cellSize + q * zx] == 0)
                    {
                        map[stalo.Location.Y / cellSize, stalo.Location.X / cellSize] = 0;
                        return true;
                    }
                map[stalo.Location.Y / cellSize, stalo.Location.X / cellSize] = 0;
            }

            return false;
        }
    }
}