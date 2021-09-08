using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Snakes_and_Ladders
{
    //x, y coordinates for position of player
    public struct BoardPosition
    {
        public int x;
        public int y;
    }
    //snakes and ladders board squares - will call them board elements
    public struct BoardElement
    {
        // 0 do nothing, 1 top of snake, 2 bottom ladder
        public int type;
        // where to go to next if type 1 or 2
        public BoardPosition nextPosition;
    }

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Player1.Visible = false;
            Player2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            pictureBox1.Visible = false;
            pictureBox3.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            pictureBox10.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            button6.Visible = false;
            button6.BringToFront();
            textBox5.BringToFront();
            linkLabel1.BringToFront();
            pictureBox11.Visible = false;
            textBox16.Visible = false;
            linkLabel1.Visible = false;
            //initialize player1 and player2 to (0,0)
            player1Position.x = 0;
            player2Position.x = 0;
            player1Position.y = 0;
            player2Position.y = 0;
            


        }

        //Define player 1 and 2 position variables 
        public BoardPosition player1Position = new BoardPosition();
        public BoardPosition player2Position = new BoardPosition();
        // Define the Board as a two dimensional matrix of board elements
        public BoardElement[,] theBoard = new BoardElement[10, 10];
        //Flags to indicate that game is finished
        public bool gameFinished = false;
        public int winner = 0;
        //User will select 1 of 5 boards
        public int selectedBoard = 0;
        //Sets scoreboard to 0
        public double numberOfRolls = 0;
        public double highScore = 0;
        public bool firstTime = true;
        public int Player1Score = 0;
        public int Player2Score = 0;
        public double highScore2 = 0;
        public bool firstTime2 = true;
        public int RollDice(BoardPosition player, PictureBox diceBox)
        {
            numberOfRolls = numberOfRolls + 0.5;
            textBox10.Text = Convert.ToString(numberOfRolls);
            
            
            Random dice = new Random();
            int num = dice.Next(1, 7);
            //Show dice picture depending upon random roll 
            switch (num)
            {
                case 1:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_1;
                    break;
                case 2:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_2;
                    break;
                case 3:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_3;
                    break;
                case 4:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_4;
                    break;
                case 5:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_5;
                    break;
                case 6:
                    diceBox.Image = Snakes_and_Ladders.Properties.Resources.Dice_6;
                    break;

            }

            
            // Return the number rolled
            return num;
        }
        public BoardPosition GetNextPosition(BoardPosition player, int num, TextBox coordBox, TextBox rollBox)
        {
            BoardPosition values = new BoardPosition();
            
            bool evenrow;
            //determine whether current row is an even row
            evenrow = (player.y % 2 == 0);
            if (evenrow)
            {
                values.x = player.x + num;
                values.y = player.y;
                if (values.x > 9)
                {
                    values.y = values.y + 1;
                    values.x = 9 - (values.x - 10);
                }
            }
            else 
            {
                values.x = player.x - num;
                values.y = player.y;
                if (values.x < 0)
                {
                    values.y = values.y + 1;
                    values.x = (-1 - values.x);
                }
                
            }
            
            if (values.y >= 10)
            {
                gameFinished = true;
                return values;
            }
            if (values.x == 0)
            {
                if (values.y == 9)
                {
                    gameFinished = true;
                    return values;
                }
                return values;
            }
            else
            {
                // Display the location of the player
                rollBox.Text = "roll:" + Convert.ToString(num);
                coordBox.Text = "P: (" + Convert.ToString(values.x) + "," +
                                Convert.ToString(values.y) + ")";
                

                return values;
            }
        }
       
        public BoardPosition ApplySnakeOrLadder(BoardPosition player, TextBox coordBox)
        {

            BoardPosition values = new BoardPosition();

            //If nothing on board space then exit the function
            if (theBoard[player.x,player.y].type == 0)
            {
                values.x = player.x;
                values.y = player.y;
                return values;
            }
            else 
            {
                values.x = theBoard[player.x, player.y].nextPosition.x;
                values.y = theBoard[player.x, player.y].nextPosition.y;
            }

            // Display the location of the player
            coordBox.Text = "P: (" + Convert.ToString(values.x) + "," +
                                Convert.ToString(values.y) + ")";
            return values;

        }
        public void DrawPlayer (BoardPosition player, PictureBox pBox, PictureBox rBox)
        {
            //Convert BoardPosition to location coordinates
            pBox.Location = rBox.Location;
            pBox.Left += player.x * 65;//One square is 64 by 64; 
            pBox.Top -= player.y * 65;
           
        }


       
        public async void EndGame (int winner)
        {
            // Indicate which player wins and reset
            pictureBox4.Visible = true;
            textBox5.Visible = true;
            pictureBox10.Visible = true;
            textBox5.Text = "Player " + (Convert.ToString(winner)) + " is winner";
            button1.Visible = false;
            button4.Visible = false;
            gameFinished = false;
            textBox10.Text = Convert.ToString(numberOfRolls);

            if (winner == 1) 
            { 
                player1Position.x = 0; 
                player1Position.y = 9; 
                Player1Score = Player1Score + 1;
                pictureBox10.Image = Snakes_and_Ladders.Properties.Resources.Player1Win1;
            }
            if (winner == 2) 
            { 
                player2Position.x = 0; 
                player2Position.y = 9; 
                Player2Score = Player2Score + 1;
                pictureBox10.Image = Snakes_and_Ladders.Properties.Resources.Player2Win;
            }
            textBox7.Text = "P1: " + Convert.ToString(Player1Score) + " wins";
            textBox8.Text = "P2: " + Convert.ToString(Player2Score)+ " wins";
            await Task.Delay(200);
            if (winner == 1)
            {
                if (highScore > numberOfRolls)
                {
                    
                    highScore = numberOfRolls;
                }
                if (firstTime == true)
                {
                    
                    highScore = numberOfRolls;
                    firstTime = false;
                }
                textBox12.Text = "P1: " + Convert.ToString(highScore);
            }
            if (winner == 2)
            {
                if (highScore2 > numberOfRolls)
                {
                    
                    highScore2 = numberOfRolls;
                }
                if (firstTime2 == true)
                {
                    
                    highScore2 = numberOfRolls;
                    firstTime2 = false;
                }
                textBox13.Text = "P2: " + Convert.ToString(highScore2);
            }
            await Task.Delay(300);
            numberOfRolls = 0;


            

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Snakes_and_Ladders.Properties.Resources.giphy;
            pictureBox3.Image = Snakes_and_Ladders.Properties.Resources.giphy;
            // Here is where all the action happens

            //Roll dice 1 and 2 and show pictures
            await Task.Delay(100);
            int dice1rolled = RollDice(player1Position, pictureBox1);
            await Task.Delay(100);
            int dice2rolled = RollDice(player2Position, pictureBox3);
           
            //Determine the next position of p1 and p2 and display
            player1Position = GetNextPosition(player1Position, dice1rolled, textBox1, textBox3);
            
            if (gameFinished) EndGame(1);
            
            else
            {
                
                
                player2Position = GetNextPosition(player2Position, dice2rolled, textBox2, textBox4);
                if (gameFinished) EndGame(2);
            }
            
            
            //Apply snake or ladder and display
            if (!(gameFinished))
                {
                player1Position = ApplySnakeOrLadder(player1Position, textBox1);
                DrawPlayer(player1Position, Player1, pictureBox6);
                
                player2Position = ApplySnakeOrLadder(player2Position, textBox2);
                DrawPlayer(player2Position, Player2, pictureBox5);
                
            }
                       
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void TIle12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Reset the game
            button1.Visible = true;
            pictureBox1.Image = Snakes_and_Ladders.Properties.Resources.Dice_1;
            pictureBox3.Image = Snakes_and_Ladders.Properties.Resources.Dice_1;
            textBox3.Text = "roll:";
            textBox4.Text = "roll:";
            pictureBox4.Visible = false;
            textBox5.Visible = false;
            Player1.Location = pictureBox6.Location;
            Player2.Location = pictureBox5.Location;
            numberOfRolls = 0;
            textBox10.Text = Convert.ToString(numberOfRolls);
            pictureBox10.Visible = false;
            button4.Visible = true;
            //initialize player1 and player2 to (0,0)
            player1Position.x = 0;
            player2Position.x = 0;
            player1Position.y = 0;
            player2Position.y = 0;
            textBox1.Text = "P: (0,0)";
            textBox2.Text = "P: (0,0)";


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Play Button
            // Initialise the selected board
            InitialiseSelectedBoard();
            switch (selectedBoard)
            {
                case 0:
                    pictureBox2.Image = Snakes_and_Ladders.Properties.Resources.Grid;
                    break;
                case 1:
                    pictureBox2.Image = Snakes_and_Ladders.Properties.Resources.Grid_2;
                    break;
                case 2:
                    pictureBox2.Image = Snakes_and_Ladders.Properties.Resources.Grid_Stage_1;
                    break;
                case 3:
                    pictureBox2.Image = Snakes_and_Ladders.Properties.Resources.Grid_3;
                    break;
                case 4:
                    pictureBox2.Image = Snakes_and_Ladders.Properties.Resources.Grid_4;
                    break;
            }
            textBox5.Visible = false;
            pictureBox4.Visible = false;
            button3.Visible = false;
            Player1.Visible = true;
            Player2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            pictureBox1.Visible = true;
            pictureBox3.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            Difficulty.Visible = false;
            pictureBox7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            button2.Visible = true;
            textBox14.Visible = true;
            textBox15.Visible = true;
            button5.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;


        }

        

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Player2_Click(object sender, EventArgs e)
        {

        }

        private void Player1_Click(object sender, EventArgs e)
        {

        }

        private void Difficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = Difficulty.SelectedIndex;
            selectedBoard = selectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    pictureBox7.Image = Snakes_and_Ladders.Properties.Resources.Grid;
                    break;
                case 1:
                    pictureBox7.Image = Snakes_and_Ladders.Properties.Resources.Grid_2;
                    break;
                case 2:
                    pictureBox7.Image = Snakes_and_Ladders.Properties.Resources.Grid_Stage_1;
                    break;
                case 3:
                    pictureBox7.Image = Snakes_and_Ladders.Properties.Resources.Grid_3;
                    break;
                case 4:
                    pictureBox7.Image = Snakes_and_Ladders.Properties.Resources.Grid_4;
                    break;
            }
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Open Start Menu Button
            textBox5.Visible = true;
            pictureBox4.Visible = true;
            button3.Visible = true;
            Difficulty.Visible = true;
            pictureBox7.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            pictureBox8.Visible = true;
            pictureBox9.Visible = true;
            button7.Visible = true;
            button2.Visible = false;
            textBox5.Text = "Snakes and Ladders";
            button5.Visible = true;
            button8.Visible = true;
            button9.Visible = true;

        }

        public void InitialiseSelectedBoard()
        {
            //Initialize the board - all squares of type 0 (do nothing) initially
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    theBoard[x, y].type = 0;
                }
            }
            switch (selectedBoard)
            {
                case 0://no stress board
                    break;
                case 1://easy board
                    //Set the bottom of ladders
                    //First ladder @ position 1,1
                    theBoard[1, 1].type = 2;
                    theBoard[1, 1].nextPosition.x = 2;
                    theBoard[1, 1].nextPosition.y = 2;
                    //Second ladder @ position 2,3
                    theBoard[2, 3].type = 2;
                    theBoard[2, 3].nextPosition.x = 5;
                    theBoard[2, 3].nextPosition.y = 5;
                    //Third ladder @ position 2,4
                    theBoard[2, 4].type = 2;
                    theBoard[2, 4].nextPosition.x = 1;
                    theBoard[2, 4].nextPosition.y = 5;
                    //Fourth ladder @ position 3,6
                    theBoard[3, 6].type = 2;
                    theBoard[3, 6].nextPosition.x = 3;
                    theBoard[3, 6].nextPosition.y = 9;
                    //Fifth ladder @ position 9,6
                    theBoard[9, 6].type = 2;
                    theBoard[9, 6].nextPosition.x = 8;
                    theBoard[9, 6].nextPosition.y = 7;
                    //Set the top of snakes
                    //First snake @ 1,3
                    theBoard[1, 3].type = 1;
                    theBoard[1, 3].nextPosition.x = 6;
                    theBoard[1, 3].nextPosition.y = 0;
                    //Second snake @ 5,4
                    theBoard[5, 4].type = 1;
                    theBoard[5, 4].nextPosition.x = 8;
                    theBoard[5, 4].nextPosition.y = 6;
                    //Third snake @ 2,8
                    theBoard[2, 8].type = 1;
                    theBoard[2, 8].nextPosition.x = 1;
                    theBoard[2, 4].nextPosition.y = 9;
                    break;
                case 2://medium board
                    //Set the bottom of ladders
                    //First ladder @ position 9,1
                    theBoard[9, 1].type = 2;
                    theBoard[9, 1].nextPosition.x = 9;
                    theBoard[9, 1].nextPosition.y = 3;
                    //Second ladder @ position 4,2
                    theBoard[4, 2].type = 2;
                    theBoard[4, 2].nextPosition.x = 3;
                    theBoard[4, 2].nextPosition.y = 4;
                    //Third ladder @ position 0,4
                    theBoard[0, 4].type = 2;
                    theBoard[0, 4].nextPosition.x = 1;
                    theBoard[0, 4].nextPosition.y = 5;
                    //Fourth Ladder @ position 5,4
                    theBoard[5, 4].type = 2;
                    theBoard[5, 4].nextPosition.x = 2;
                    theBoard[5, 4].nextPosition.y = 8;
                    //Fifth Ladder @ position 9,7
                    theBoard[9, 7].type = 2;
                    theBoard[9, 7].nextPosition.x = 8;
                    theBoard[9, 7].nextPosition.y = 9;
                    //Set the top of Snakes
                    //First Snake at 3,1
                    theBoard[3, 1].type = 1;
                    theBoard[3, 1].nextPosition.x = 4;
                    theBoard[3, 1].nextPosition.y = 0;
                    //Second Snake at 8,3
                    theBoard[8, 3].type = 1;
                    theBoard[8, 3].nextPosition.x = 7;
                    theBoard[8, 3].nextPosition.y = 0;
                    //Third Snake at 3,5
                    theBoard[3, 5].type = 1;
                    theBoard[3, 5].nextPosition.x = 0;
                    theBoard[3, 5].nextPosition.y = 3;
                    //Fourth Snake at 4,7
                    theBoard[4, 7].type = 1;
                    theBoard[4, 7].nextPosition.x = 6;
                    theBoard[4, 7].nextPosition.y = 5;
                    //Fifth Snake at 3,9
                    theBoard[3, 9].type = 1;
                    theBoard[3, 9].nextPosition.x = 8;
                    theBoard[3, 9].nextPosition.y = 6;
                    break;
                case 3://hard board
                    //First ladder @ position 1,0
                    theBoard[1, 0].type = 2;
                    theBoard[1, 0].nextPosition.x = 1;
                    theBoard[1, 0].nextPosition.y = 2;
                    //Second ladder @ position 3,3
                    theBoard[3, 3].type = 2;
                    theBoard[3, 3].nextPosition.x = 4;
                    theBoard[3, 3].nextPosition.y = 4;
                    //Third ladder @ position 1,4
                    theBoard[1, 4].type = 2;
                    theBoard[1, 4].nextPosition.x = 3;
                    theBoard[1, 4].nextPosition.y = 6;
                    //Fourth ladder @ position 4,6
                    theBoard[4, 6].type = 2;
                    theBoard[4, 6].nextPosition.x = 4;
                    theBoard[4, 6].nextPosition.y = 8;
                    //Fifth ladder @ position 8,6
                    theBoard[8, 6].type = 2;
                    theBoard[8, 6].nextPosition.x = 9;
                    theBoard[8, 6].nextPosition.y = 7;
                    //Sixth ladder @ position 2,8
                    theBoard[2, 8].type = 2;
                    theBoard[2, 8].nextPosition.x = 3;
                    theBoard[2, 8].nextPosition.y = 9;
                    //Set the top of Snakes
                    //First Snake at 2,2
                    theBoard[2, 2].type = 1;
                    theBoard[2, 2].nextPosition.x = 3;
                    theBoard[2, 2].nextPosition.y = 1;
                    //Second Snake at 4,3
                    theBoard[4, 3].type = 1;
                    theBoard[4, 3].nextPosition.x = 6;
                    theBoard[4, 3].nextPosition.y = 1;
                    //Third Snake at 3,5
                    theBoard[3, 5].type = 1;
                    theBoard[3, 5].nextPosition.x = 8;
                    theBoard[3, 5].nextPosition.y = 2;
                    //Fourth Snake at 0,7
                    theBoard[0, 7].type = 1;
                    theBoard[0, 7].nextPosition.x = 1;
                    theBoard[0, 7].nextPosition.y = 5;
                    //Fifth Snake at 8,8
                    theBoard[8, 8].type = 1;
                    theBoard[8, 8].nextPosition.x = 5;
                    theBoard[8, 8].nextPosition.y = 6;
                    //Sixth Snake at 7,9
                    theBoard[7, 9].type = 1;
                    theBoard[7, 9].nextPosition.x = 4;
                    theBoard[7, 9].nextPosition.y = 7;
                    break;
                case 4://hardcore board
                    //First ladder @ position 3,0
                    theBoard[3, 0].type = 2;
                    theBoard[3, 0].nextPosition.x = 3;
                    theBoard[3, 0].nextPosition.y = 2;
                    //Second ladder @ position 6,2
                    theBoard[6, 2].type = 2;
                    theBoard[6, 2].nextPosition.x = 3;
                    theBoard[6, 2].nextPosition.y = 6;
                    //Third ladder @ position 1,3
                    theBoard[1, 3].type = 2;
                    theBoard[1, 3].nextPosition.x = 0;
                    theBoard[1, 3].nextPosition.y = 4;
                    //Fourth ladder @ position 9,6
                    theBoard[9, 6].type = 2;
                    theBoard[9, 6].nextPosition.x = 8;
                    theBoard[9, 6].nextPosition.y = 7;
                    //Fifth ladder @ position 5,7
                    theBoard[5, 7].type = 2;
                    theBoard[5, 7].nextPosition.x = 4;
                    theBoard[5, 7].nextPosition.y = 8;
                    //Sixth ladder @ position 1,7
                    theBoard[1, 7].type = 2;
                    theBoard[1, 7].nextPosition.x = 1;
                    theBoard[1, 7].nextPosition.y = 9;
                    //Set the top of Snakes
                    //First Snake at 9,2    
                    theBoard[9, 2].type = 1;
                    theBoard[9, 2].nextPosition.x = 6;
                    theBoard[9, 2].nextPosition.y = 0;
                    //Second Snake at 3,3    
                    theBoard[3, 3].type = 1;
                    theBoard[3, 3].nextPosition.x = 1;
                    theBoard[3, 3].nextPosition.y = 1;
                    //Third Snake at 1,4    
                    theBoard[1, 4].type = 1;
                    theBoard[1, 4].nextPosition.x = 1;
                    theBoard[1, 4].nextPosition.y = 6;
                    //Fourth Snake at 7,5    
                    theBoard[7, 5].type = 1;
                    theBoard[7, 5].nextPosition.x = 9;
                    theBoard[7, 5].nextPosition.y = 3;
                    //Fifth Snake at 1,6    
                    theBoard[1, 6].type = 1;
                    theBoard[1, 6].nextPosition.x = 3;
                    theBoard[1, 6].nextPosition.y = 4;
                    //Sixth Snake at 2,7    
                    theBoard[2, 7].type = 1;
                    theBoard[2, 7].nextPosition.x = 7;
                    theBoard[2, 7].nextPosition.y = 4;
                    //Seventh Snake at 3,8    
                    theBoard[3, 8].type = 1;
                    theBoard[3, 8].nextPosition.x = 0;
                    theBoard[3, 8].nextPosition.y = 6;
                    //Eighth Snake at 3,9    
                    theBoard[3, 9].type = 1;
                    theBoard[3, 9].nextPosition.x = 8;
                    theBoard[3, 9].nextPosition.y = 6;
                    //Ninth Snake at 7,9    
                    theBoard[7, 9].type = 1;
                    theBoard[7, 9].nextPosition.x = 9;
                    theBoard[7, 9].nextPosition.y = 7;

                    break;
            }

            
        
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Player 2 Select
            int selectedIndex2 = comboBox1.SelectedIndex;
            switch (selectedIndex2)
            {
                case 0:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Prototype_Player;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Prototype_Player;
                    break;
                case 1:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Player_2_2;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Player_2_2;
                    break;
                case 2:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Player_2_3;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Player_2_3;
                    break;
                case 3:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Player_2_4;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Player_2_4;
                    break;
                case 4:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Player_2_5;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Player_2_5;
                    break;
                case 5:
                    pictureBox9.Image = Snakes_and_Ladders.Properties.Resources.Player_2_6;
                    Player2.Image = Snakes_and_Ladders.Properties.Resources.Player_2_6;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Player 1 Select
            int selectedIndex2 = comboBox2.SelectedIndex;
            switch (selectedIndex2)
            {
                case 0:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Prototype_Player_2;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Prototype_Player_2;
                    break;
                case 1:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Player_1_2;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Player_1_2;
                    break;
                case 2:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Player_1_3;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Player_1_3;
                    break;
                case 3:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Player_1_4;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Player_1_4;
                    break;
                case 4:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Player_1_5;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Player_1_5;
                    break;
                case 5:
                    pictureBox8.Image = Snakes_and_Ladders.Properties.Resources.Player_1_6;
                    Player1.Image = Snakes_and_Ladders.Properties.Resources.Player_1_6;
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Tutorial Button
            button3.Visible = false;
            Difficulty.Visible = false;
            pictureBox7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            button5.Visible = false;
            button6.Visible = true;
            button4.Visible = false;
            button7.Visible = false;
            pictureBox11.Visible = true;
            textBox16.Visible = true;
            linkLabel1.Visible = true;
            button8.Visible = false;
            button9.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Return from Tutorial Button
            button3.Visible = true;
            Difficulty.Visible = true;
            pictureBox7.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            pictureBox8.Visible = true;
            pictureBox9.Visible = true;
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = true;
            button4.Visible = true;
            pictureBox11.Visible = false;
            textBox16.Visible = false;
            linkLabel1.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Reset Scoreboard button
            highScore = 0;
            highScore2 = 0;
            firstTime = true;
            firstTime2 = true;
            Player1Score = 0;
            Player2Score = 0;
            textBox7.Text = "P1: " + Convert.ToString(Player1Score) + " wins";
            textBox8.Text = "P2: " + Convert.ToString(Player2Score) + " wins";
            textBox12.Text = "P1: " + Convert.ToString(highScore);
            textBox13.Text = "P2: " + Convert.ToString(highScore2);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sites.google.com/view/snakes-and-ladders-tutorial/how-to-play");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Choose own picture for player 1
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Player1.Image = new Bitmap(open.FileName);
                pictureBox8.Image = new Bitmap(open.FileName);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Choose own picture for player 2
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Player2.Image = new Bitmap(open.FileName);
                pictureBox9.Image = new Bitmap(open.FileName);
            }
        }
    }

}
