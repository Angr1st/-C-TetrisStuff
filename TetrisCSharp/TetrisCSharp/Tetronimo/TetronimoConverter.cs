using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TetrisCSharp
  {
  internal class TetronimoConverter
    {
    //Zweiter Converter für neuen Tetronimo im Grid
    public List<int> ConvertToGameGrid(Tetronimo tetronimo)
      {
      List<int> resultList = new List<int>( 4 );
      switch ( tetronimo.TetronimoType )
        {
        case TetronimoType.I:
          resultList.Add( 3105 );
          resultList.Add( 3205 );
          resultList.Add( 3305 );
          resultList.Add( 3405 );
          break;

        case TetronimoType.J:
          resultList.Add(3105);
          resultList.Add(3205);
          resultList.Add(3305);
          resultList.Add(3304);
          break;

        case TetronimoType.L:
          resultList.Add(3105);
          resultList.Add(3205);
          resultList.Add(3305);
          resultList.Add(3306);
          break;

        case TetronimoType.O:
          resultList.Add(3105);
          resultList.Add(3106);
          resultList.Add(3205);
          resultList.Add(3206);
          break;

        case TetronimoType.S:
          resultList.Add(3105);
          resultList.Add(3106);
          resultList.Add(3204);
          resultList.Add(3205);
          break;

        case TetronimoType.T:
          resultList.Add(3104);
          resultList.Add(3105);
          resultList.Add(3106);
          resultList.Add(3205);
          break;

        case TetronimoType.Z:
          resultList.Add(3104);
          resultList.Add(3105);
          resultList.Add(3205);
          resultList.Add(3206);
          break;

        case TetronimoType.Unknown:
          break;
        default:
          break;
        }
      return resultList;
      }

    public static System.Drawing.Image SelectBlockImage(GameTileColors color)
      {
      switch ( color )
        {
        case GameTileColors.Blau:
          return TetrisCSharp.Properties.Resources.Blau;

        case GameTileColors.Gelb:
          return TetrisCSharp.Properties.Resources.Gelb;

        case GameTileColors.Gruen:
          return TetrisCSharp.Properties.Resources.Grün;

        case GameTileColors.Lila:
          return TetrisCSharp.Properties.Resources.Lila;

        case GameTileColors.Orange:
          return TetrisCSharp.Properties.Resources.Orange;

        case GameTileColors.Rot:
          return TetrisCSharp.Properties.Resources.Rot;

        case GameTileColors.Tuerkis:
          return TetrisCSharp.Properties.Resources.Türkis;

        case GameTileColors.Schwarz:
          return null;//Da hier BackgroundColor verwendet wird
        default:
          return null;
        }
      }

    public List<PictureBox> ConvertToPictureBox( Tetronimo tetronimo )
      {
      List<PictureBox> result = new List<PictureBox>( 4 );
      for ( int i = 0; i < 4; i++ )
        {
        var newPictureBox = new PictureBox();
        newPictureBox.Size = new System.Drawing.Size( 20, 20 );
        newPictureBox.Location = tetronimo.Form[i];
        newPictureBox.Image = SelectBlockImage( tetronimo.Color );
        newPictureBox.BackColor = Color.Black;
        result.Add( newPictureBox );
        }
      return result;
      }
    }
  }