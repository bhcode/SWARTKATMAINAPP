using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Label
{
    int row;
    string labelText;

    public int Row
    {
        get
        {
            return row;
        }

        set
        {
            row = value;
        }
    }

    public string LabelText
    {
        get
        {
            return labelText;
        }

        set
        {
            labelText = value;
        }
    }

    public Label(int row, string label)
    {
        Row = row;
        LabelText = label;
    }
}