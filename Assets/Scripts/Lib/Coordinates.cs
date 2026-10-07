public class Coordinates
{
	public int x {get; private set;}
	public int y {get; private set;}
	
	public Coordinates(int x, int y)
	{
		this.x = x;
		this.y = y;
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
		
		return other.x == x && other.y == y;
	}
	
}