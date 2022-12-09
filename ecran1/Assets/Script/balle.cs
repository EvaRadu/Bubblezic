using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace BalleGrandiRetreci
{
  public class Balle
  {
    int xPos = 50;
    int yPos = 50;
    int size = 20;
    int xSpeed = 5;
    int ySpeed = 5;

    public void MovementBalle()
    {
      xPos += xSpeed;
      yPos += ySpeed;

      if (xPos > Form1.ActiveForm.Width - size || xPos < 0)
      {
        xSpeed = -xSpeed;
      }

      if (yPos > Form1.ActiveForm.Height - size || yPos < 0)
      {
        ySpeed = -ySpeed;
      }

      if (size > 100)
      {
        size = 20;
      }
      else
      {
        size++;
      }
    }

    public void DessineBalle(Graphics g)
    {
      g.FillEllipse(Brushes.Red, xPos, yPos, size, size);
    }
  }

  public class BalleForm : Form
  {
    Balle balle = new Balle();

    public BalleForm()
    {
      this.Paint += new PaintEventHandler(this.OnPaint);
      this.DoubleBuffered = true;
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
      balle.MovementBalle();
      balle.DessineBalle(e.Graphics);

      Invalidate();
    }
  }

  static class Program
  {
    static void Main()
    {
      Application.Run(new BalleForm());
    }
  }
}
