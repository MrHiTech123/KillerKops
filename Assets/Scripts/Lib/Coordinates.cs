public class Coordinates
{
	public int col {get; private set;}
	public int row {get; private set;}
	
	public Coordinates(int col, int row)
	{
		this.col = col;
		this.row = row;
	}
		
	// override object.Equals
	public override bool Equals(object obj)
	{
		//
		// See the full list of guidelines at
		//   http://go.microsoft.com/fwlink/?LinkID=85237
		// and also the guidance for operator== at
		//   http://go.microsoft.com/fwlink/?LinkId=85238
		//
		
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		
		// TODO: write your implementation of Equals() here
		Coordinates other = (Coordinates)obj;
		
		return other.col == col && other.row == row;
		
	}
	public override int GetHashCode()
	{
		return col + row;
	}
	
}