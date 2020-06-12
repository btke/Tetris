using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using static System.Console;
using static State;
using static GameStatus;

enum State{Empty, Falling, Fixed};
enum GameStatus{Intro, Game, Gameover};

namespace project
{
    class Position{
        public int x, y;
        public Position(int x, int y){ this.x = x; this.y = y;}
    }

    abstract class Tetromino{
        public abstract Brush brush {get;}
        public abstract Position[][] orientations {get;} //The first Position in one orientation is the pivot
        public abstract float[] centreRelativeToPivot {get;}
        public abstract void orientationTransition(Playfield playfield, KeyEventArgs e);
    }

    class I: Tetromino{
        // I's pivot is as the following
        // IPII
        public override Brush brush {
            get{
                return Brushes.LightSkyBlue;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[3][];
                o[1] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(1, 0), new Position(2, 0)};
                o[2] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(0, 1), new Position(0, 2)};
                return o;
            }
        }
        public override float[] centreRelativeToPivot {
            get{
                return new float[3]{0, (float)0.5, 0};
            }
        }
        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if  (
                    playfield.currentTetrominoPivotPosition.y + 2 < Playfield.playfieldHeight &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x,     playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x,     playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x,     playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                ){
                    playfield.changeCurrentTetrominoOrientation(2);
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if  (
                        playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                        playfield.currentTetrominoPivotPosition.x >= 1 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                }
            }
        }
    }

    class J: Tetromino{
        // J's pivot is as the following
        // J
        // PJJ
        public override Brush brush {
            get{
                return Brushes.DarkBlue;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[5][];
                o[1] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(1, 0), new Position(2, 0)};
                o[2] = new Position[4]{new Position(0, 0), new Position(0, 1), new Position(0, 2), new Position(1, 0)};
                o[3] = new Position[4]{new Position(0, 0), new Position(-2, 0), new Position(-1, 0), new Position(0, 1)};
                o[4] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, -2), new Position(0, -1)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[5]{0, 1, (float)0.5, -1, (float)-0.5};
            }
        }

        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if (e.KeyCode == Keys.D){
                    if  (
                        playfield.currentTetrominoPivotPosition.y + 2 < Playfield.playfieldHeight &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x,     playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x,     playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x >= 1 &&
                        playfield.currentTetrominoPivotPosition.y - 2 >= 0  &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x - 2 >= 0 &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                        playfield.currentTetrominoPivotPosition.y >= 1 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 3){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.y >= 2 &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 2] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x + 1 < Playfield.playfieldWidth &&
                        playfield.currentTetrominoPivotPosition.y + 2 < Playfield.playfieldHeight &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 4){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y    ] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x >= 2 &&
                        playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
            }
        }
    }

    class L: Tetromino{
        // L's pivot is as the follwoing
        //   L
        // LLP
        public override Brush brush {
            get{
                return Brushes.Orange;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[5][];
                o[1] = new Position[4]{new Position(0, 0), new Position(-2, 0), new Position(-1, 0), new Position(0, -1)};
                o[2] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(0, -2), new Position(1, 0)};
                o[3] = new Position[4]{new Position(0, 0), new Position(0, 1), new Position(1, 0), new Position(2, 0)};
                o[4] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, 1), new Position(0, 2)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[5]{0, -1, (float)0.5, 1, (float)-0.5};
            }
        }

        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if (e.KeyCode == Keys.D){
                    if  (
                        playfield.currentTetrominoPivotPosition.y >= 2 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.y + 2 < Playfield.playfieldHeight  &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 2] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                            playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y    ] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x >= 2 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 3){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x >= 1 &&
                            playfield.currentTetrominoPivotPosition.y + 2 < Playfield.playfieldHeight &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.y >= 2 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 4){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 1] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x + 2 < Playfield.playfieldWidth &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 2] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 2, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
            }
        }
    }

    class O: Tetromino{
        // O's pivot is as the following
        // OO
        // PO
        public override Brush brush {
            get{
                return Brushes.Yellow;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[2][];
                o[1] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(1, -1), new Position(1, 0)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[2]{0, (float)0.5};
            }
        }

        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            
        }
    }

    class S: Tetromino{
        // S's pivot is as the following
        //  SS
        // SP
        public override Brush brush {
            get{
                return Brushes.LawnGreen;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[3][];
                o[1] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, -1), new Position(1, -1)};
                o[2] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(1, 0), new Position(1, 1)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[3]{0, 0, (float)0.5};
            }
        }
        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if  (
                    playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                ){
                    playfield.changeCurrentTetrominoOrientation(2);
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if  (
                        playfield.currentTetrominoPivotPosition.x >= 1 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                }
            }
        }
    }

    class T: Tetromino{
        // T's pivot is as the follwoing
        //  T
        // TPT
        public override Brush brush {
            get{
                return Brushes.Purple;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[5][];
                o[1] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, -1), new Position(1, 0)};
                o[2] = new Position[4]{new Position(0, 0), new Position(0, -1), new Position(0, 1), new Position(1, 0)};
                o[3] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, 1), new Position(1, 0)};
                o[4] = new Position[4]{new Position(0, 0), new Position(-1, 0), new Position(0, -1), new Position(0, 1)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[5]{0, 0, (float)0.5, 0, (float)-0.5};
            }
        }

        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if (e.KeyCode == Keys.D){
                    if  (
                        playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight  &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x >= 1 &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x >= 1 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 3){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.y >= 1 &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(4);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.y >= 2 &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x    , playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(2);
                    }
                }
            }
            else if (playfield.currentTetrominoOrientation == 4){
                if (e.KeyCode == Keys.D){
                    if  (
                            playfield.currentTetrominoPivotPosition.x + 1 < Playfield.playfieldWidth &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                            playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty
                        ){
                            playfield.changeCurrentTetrominoOrientation(1);
                    }
                }
                else if (e.KeyCode == Keys.A){
                    if  (
                        playfield.currentTetrominoPivotPosition.x + 1 < Playfield.playfieldWidth &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(3);
                    }
                }
            }
        }
    }

    class Z: Tetromino{
        // Z's pivot is as the following
        // ZZ
        //  PZ
        public override Brush brush {
            get{
                return Brushes.Red;
            }
        }
        public override Position[][] orientations {
            
            get{
                Position[][] o = new Position[3][];
                o[1] = new Position[4]{new Position(0, 0), new Position(-1, -1), new Position(0, -1), new Position(1, 0)};
                o[2] = new Position[4]{new Position(0, 0), new Position(-1, 1), new Position(-1, 0), new Position(0, -1)};
                return o;
            }
        }

        public override float[] centreRelativeToPivot {
            get{
                return new float[3]{0, 0, (float)-0.5};
            }
        }

        public override void orientationTransition(Playfield playfield, KeyEventArgs e){
            if (playfield.currentTetrominoOrientation == 1){
                if  (
                    playfield.currentTetrominoPivotPosition.y + 1 < Playfield.playfieldHeight &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y + 1] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y    ] == Empty &&
                    playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty
                ){
                    playfield.changeCurrentTetrominoOrientation(2);
                }
            }
            else if (playfield.currentTetrominoOrientation == 2){
                if  (
                        playfield.currentTetrominoPivotPosition.x + 1 <Playfield.playfieldWidth &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x - 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y - 1] == Empty &&
                        playfield.grid[playfield.currentTetrominoPivotPosition.x + 1, playfield.currentTetrominoPivotPosition.y    ] == Empty
                    ){
                        playfield.changeCurrentTetrominoOrientation(1);
                }
            }
        }
    }



    class Playfield{
        public const int playfieldWidth = 10;
        public const int playfieldHeight = 20;
        public State[,] grid = new State[playfieldWidth, playfieldHeight];
        public int currentTetrominoOrientation = 1; // ranges from 1 to at most 4
        public int nextTetrominoOrientation = 1; // ranges from 1 to at most 4
        public Position currentTetrominoPivotPosition = new Position(4, 2);
        public Tetromino currentTetromino = newTetrominoRandomGenerator();
        public Tetromino nextTetromino = newTetrominoRandomGenerator();
        public Timer myTimer = new Timer();
        public delegate void Notify();
        public event Notify changed;
        public GameStatus gameStatus = Intro;
        public int score = 0;
        public int currentLevel = 1;
        public int lines = 0;

        public Playfield(){
            for (int x = 0; x < playfieldWidth; ++x){
                for (int y = 0; y < playfieldHeight; ++y){
                    grid[x, y] = Empty;
                }
            }
            myTimer.Interval = 1000;
            myTimer.Tick += new EventHandler(onTick);
        }
        void onTick(object sender, EventArgs e){
            if  (
                currentTetrominoOneStepMovable(0, 1)
                ){
                ++currentTetrominoPivotPosition.y;
                changed();
            }
            else {
                fixCurrentTetrominoAndClearLines();
                nextTurn();
            }
        }
        static Tetromino newTetrominoRandomGenerator(){
            Random ran = new Random();
            int tetrominoRepresentationNumber = ran.Next(1, 8);
            switch (tetrominoRepresentationNumber){
                case 1: return new I();
                case 2: return new J();
                case 3: return new L();
                case 4: return new O();
                case 5: return new S();
                case 6: return new T();
                case 7: return new Z();
                default: return null;
            }
        }
        public bool currentTetrominoOneStepMovable(int xDirection, int yDirection){
            Position[][] orientations = currentTetromino.orientations;
            foreach (Position p in orientations[currentTetrominoOrientation]){
                int x = currentTetrominoPivotPosition.x + p.x + xDirection;
                int y = currentTetrominoPivotPosition.y + p.y + yDirection;
                if (
                    x < 0 ||
                    y < 0 ||
                    x >= playfieldWidth ||
                    y >= playfieldHeight ||
                    grid[x, y] == Fixed
                    ){
                    return false;
                }
            }
            return true;
        }
        public void nextTurn(){
            Random ran = new Random();

            currentTetromino = nextTetromino;
            currentTetrominoOrientation = nextTetrominoOrientation;
            currentTetrominoPivotPosition = new Position(4, 2);

            nextTetromino = newTetrominoRandomGenerator();
            nextTetrominoOrientation = ran.Next(1, nextTetromino.orientations.Length);
            changed();
        }
        public void restart(){
            grid = new State[playfieldWidth, playfieldHeight];
            nextTurn();
            changed();
        }
        public void fixCurrentTetrominoAndClearLines(){
            Position[][] orientations = currentTetromino.orientations;
            foreach (Position p in orientations[currentTetrominoOrientation]){
                if (grid[currentTetrominoPivotPosition.x + p.x, currentTetrominoPivotPosition.y + p.y] == Fixed){
                    myTimer.Stop();
                    restart();
                    gameStatus = Gameover;
                    break;
                }
                else{
                    grid[currentTetrominoPivotPosition.x + p.x, currentTetrominoPivotPosition.y + p.y] = Fixed;
                }
            }
            // remeber the lines to be deleted
            List<int> linesToBeCleared = new List<int>();
            for (int y = playfieldHeight - 1; y >= 0 && linesToBeCleared.Count < 4; --y){
                bool doClear = true;
                for (int x = 0; x < playfieldWidth; ++x){
                    if(grid[x, y] != Fixed){
                        doClear = false;
                    }
                }
                if(doClear == true){
                    linesToBeCleared.Add(y);
                }
            }
            // compute the lines, scores, and levels
            lines += linesToBeCleared.Count;
            if (linesToBeCleared.Count > 0){
                score += currentLevel * (linesToBeCleared.Count - 1) * (linesToBeCleared.Count - 1) * 100 + 100;
            }
            currentLevel = (lines / 30 % 4 + 1) > 4? 4 : (lines / 30 % 4 + 1);
            myTimer.Interval = (int)(1000 * (1 - (currentLevel - 1) * 0.2));
            // created a new grid without the deleted lines
            var newGrid = new State[playfieldWidth, playfieldHeight];
            int yForNewGrid = playfieldHeight - 1;
            for (int y = playfieldHeight - 1; y >= 0; --y){
                if(!linesToBeCleared.Contains(y)){
                    for (int x = 0; x < playfieldWidth; ++x){
                        newGrid[x, yForNewGrid] = grid[x, y];
                    }
                    --yForNewGrid;
                }                
            }
            grid = newGrid;
            changed();
        }
        public void changeCurrentTetrominoPivot(int xDirection, int yDirection){
            currentTetrominoPivotPosition.x += xDirection;
            currentTetrominoPivotPosition.y += yDirection;
            changed();
        }
        public void changeCurrentTetrominoOrientation(int o){
            currentTetrominoOrientation = o;
            changed();
        }
    }

    class MyForm : Form
    {
        const int ScreenWidth = 450;
        const int ScreenHeight = 587;
        Playfield playfield = new Playfield();
        bool paused = false;
        Timer promptTimer = new Timer();
        Brush promptBrush = Brushes.LightGray;
        void onChange(){
            Invalidate();
        }
        public MyForm()
        {
            playfield.changed += onChange;
            ClientSize = new Size(ScreenWidth, ScreenHeight);
            StartPosition = FormStartPosition.CenterScreen;
            promptTimer.Interval = 300;
            promptTimer.Tick += new EventHandler(onPrompt);
            promptTimer.Start();
        }
        protected override void OnKeyDown(KeyEventArgs args)
        {
            switch(playfield.gameStatus){
                case Intro:
                    playfield.gameStatus = Game;
                    playfield.myTimer.Start();
                    promptTimer.Stop();
                    break;
                case Gameover:
                    playfield.gameStatus = Game;
                    playfield.myTimer.Start();
                    promptTimer.Stop();
                    break;
                case Game:
                    if (paused == true){
                        if (args.KeyCode == Keys.F2){
                            playfield.myTimer.Enabled = !playfield.myTimer.Enabled;
                            paused = false;
                        }
                        else if (args.KeyCode == Keys.F3){
                            Application.Exit();
                        }
                        else if (args.KeyCode == Keys.F1){
                            paused = false;
                            playfield.myTimer.Start();
                            playfield.restart();
                        }
            
                    }
                    else{
                        if  (
                            args.KeyCode == Keys.Left &&
                            playfield.currentTetrominoOneStepMovable(-1, 0)
                        )
                        {
                            playfield.changeCurrentTetrominoPivot(-1, 0);
                        }
                        
                        else if (
                                args.KeyCode == Keys.Right &&
                                playfield.currentTetrominoOneStepMovable(1, 0)
                                )
                        {
                            playfield.changeCurrentTetrominoPivot(1, 0);
                        }
                        else if (args.KeyCode == Keys.Down)
                        {
                            if  (
                                playfield.currentTetrominoOneStepMovable(0, 1)
                                ){
                                playfield.changeCurrentTetrominoPivot(0, 1);
                            }
                            else {
                                playfield.fixCurrentTetrominoAndClearLines();
                                playfield.nextTurn();
                            }
                            
                        }
                        else if (args.KeyCode == Keys.Space)
                        {
                            while(playfield.currentTetrominoOneStepMovable(0, 1)){
                                playfield.changeCurrentTetrominoPivot(0, 1);
                            }
                            playfield.fixCurrentTetrominoAndClearLines();
                            playfield.nextTurn();
                        }
                        else if (args.KeyCode == Keys.A || args.KeyCode == Keys.D){
                            playfield.currentTetromino.orientationTransition(playfield, args);
                        }
                        else if (args.KeyCode == Keys.F3){
                            Application.Exit();
                        }
                        else if (args.KeyCode == Keys.F2){
                            playfield.myTimer.Enabled = !playfield.myTimer.Enabled;
                            paused = true;
                        }
                        else if (args.KeyCode == Keys.F1){
                            playfield.restart();
                        }
                    }
                    break;
            }
        }
        void onPrompt(Object sender, EventArgs e){
            if (promptBrush == Brushes.LightGray){
                promptBrush = Brushes.Black;
            }
            else{
                promptBrush = Brushes.LightGray;
            }
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;

            switch (playfield.gameStatus){
                case Intro:
                    g.DrawString("Tetris", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF(ScreenWidth / 2 - 90, ScreenHeight / 2 - 60));
                    g.DrawString("Press any key to start.", new Font(FontFamily.GenericMonospace, 10), promptBrush, new PointF(ScreenWidth / 2 - 100, ScreenHeight / 2));
                    break;
                case Gameover:
                    promptTimer.Start();
                    g.DrawString("Game Over", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF(ScreenWidth / 2 - 120, ScreenHeight / 2 - 60));
                    g.DrawString("Press any key to restart.", new Font(FontFamily.GenericMonospace, 10), promptBrush, new PointF(ScreenWidth / 2 - 105, ScreenHeight / 2));
                    break;
                case Game:           
                    // draw the frame
                    g.DrawRectangle(new Pen(Color.Black, 3), new Rectangle(1, 1, 447, 584));
                    g.DrawLine(new Pen(Color.Black, 3), 295, 1, 295, 585);
                    //draw the next
                    g.DrawString("Next", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF((float)317.5, 35));

                    Position[][] nextTetriminoOrientations = playfield.nextTetromino.orientations;
                    Brush nextTetriminoBrush = playfield.nextTetromino.brush;
                    float centreRelativeToPivot = playfield.nextTetromino.centreRelativeToPivot[playfield.nextTetrominoOrientation];
                    foreach (Position p in nextTetriminoOrientations[playfield.nextTetrominoOrientation]){
                        g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(357 + p.x * 29 - (int)(centreRelativeToPivot * 29), 140 + p.y * 29, 29, 29));
                        g.FillRectangle(nextTetriminoBrush, 358 + p.x * 29 - (int)(centreRelativeToPivot * 29), 141 + p.y * 29, 28, 28);
                    }
                    //draw the score
                    g.DrawString("Score", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF((float)300, 220));
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(playfield.score.ToString(), new Font(FontFamily.GenericMonospace, 15), Brushes.Black, new Rectangle(297, 260, 150, 30), sf);

                    //draw the lines
                    g.DrawString("Lines", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF((float)300, 290));
                    g.DrawString(playfield.lines.ToString(), new Font(FontFamily.GenericMonospace, 15), Brushes.Black, new Rectangle(297, 330, 150, 30), sf);

                    //draw the level
                    g.DrawString("Level", new Font(FontFamily.GenericMonospace, 30), Brushes.Black, new PointF((float)300, 360));
                    g.DrawString(playfield.currentLevel.ToString(), new Font(FontFamily.GenericMonospace, 15), Brushes.Black, new Rectangle(297, 400, 150, 30), sf);
                    //draw the shortcut keys
                    g.DrawString("F1: Restart", new Font(FontFamily.GenericMonospace, 10), Brushes.Black, new PointF((float)300, 530));
                    g.DrawString("F2: Pause/Resume", new Font(FontFamily.GenericMonospace, 10), Brushes.Black, new PointF((float)300, 540));
                    g.DrawString("F3: Exit", new Font(FontFamily.GenericMonospace, 10), Brushes.Black, new PointF((float)300, 550));
                    // draw the fixed blocks
                    for (int y = 0; y < Playfield.playfieldHeight; ++y){
                        for (int x = 0; x < Playfield.playfieldWidth; ++x){
                            if (playfield.grid[x, y] == Fixed){
                                g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(3 + x * 29, 3 + y * 29, 29, 29));
                                g.FillRectangle(Brushes.LightGray, 4 + x * 29, 4 + y * 29, 28, 28);
                            }
                            
                        }
                    }
                    //draw the current falling tetrimino
                    Position[][] currentTetriminoOrientations = playfield.currentTetromino.orientations;
                    Brush currentTetriminoBrush = playfield.currentTetromino.brush;
                    Position currentTetriminoPivot = playfield.currentTetrominoPivotPosition;
                    
                    foreach (Position p in currentTetriminoOrientations[playfield.currentTetrominoOrientation]){
                        g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(3 + (p.x + currentTetriminoPivot.x) * 29, 3 + (p.y + currentTetriminoPivot.y) * 29, 29, 29));
                        g.FillRectangle(currentTetriminoBrush, 4 + (p.x + currentTetriminoPivot.x) * 29, 4 + (p.y + currentTetriminoPivot.y) * 29, 28, 28);
                    }
                    break;
            }
        }
    }
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form form = new MyForm();
            form.Text = "Tetris";
            Application.Run(form);
        }
    }
}
