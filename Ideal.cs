using System;
namespace Ideal
{
	class Ideal
	{
		//If the year has or not 1 week more
		public static int days(int y) => y == Math.Round(Math.Round(y * 1.242189 / 7) * 7 / 1.242189) ? 371 : 364;
		private int D,//Days since the change of millennium initiating in 0.
			_y = 2000,//Year initiating in 2000
			_d;//Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
		//The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
		public Ideal(DateTime g)//From Gregorian to ideal calendar
		{
			d=6+(int)g.Subtract(DateTime.Parse("2000-01-01")).TotalDays;
		}
		//Year and day. If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
		public Ideal(int Y, int D)
		{
			y=Y; d=D;
		}
		public int d
		{
			set
			{
				D += value-_d;
				_d = value;
				int N = days(_y);
				while (_d >= N)
				{
					_d -= N;
					N = days(++_y);
				}
				while (_d < 0) _d += days(--_y);
			}
            get
            {
				return _d;
            }
		}
		public int y
		{
			set
			{
				if (_d > 363) if (days(value) == 364)
				{
					_d -= 7; D -= 7;
				}
				while (_y < value) D += days(_y++);
				while (_y > value) D -= days(--_y);
			}
			get
            {
				return _y;
            }
		}
		//Day, week and month start at 0 internally and here 1 is added.
		public int m
        {
            get
            {
				return _d / 28 + 1;
            }
		}
		public int w
		{
			get
			{
				return _d / 7 + 1;
			}
		}
		public int dm
		{
			get
			{
				return _d % 28 + 1;
			}
		}
		public int dw
		{
			get
			{
				return _d % 7 + 1;
			}
		}
		public static bool? f=true;
		public override string ToString()
		{
			if(f==null) return (_d + 1) + " " + _y;
			bool b = (bool)f;
			return (b?dm:dw)+" "+(b?m:w)+" "+_y;
		}
		//If this date is previous or posterior than another.
		public static bool operator >(Ideal a, Ideal b) => a.D > b.D;
		public static bool operator <(Ideal a, Ideal b) => a.D < b.D;
		public static bool operator ==(Ideal a, Ideal b) => a.D == b.D;
		public static bool operator !=(Ideal a, Ideal b) => a.D != b.D;
		public static bool operator ==(Ideal a, int b) => a.D == b;
		public static bool operator !=(Ideal a, int b) => a.D != b;
		//From ideal to Gregorian calendar
		public static explicit operator DateTime(Ideal i) => DateTime.Parse("2000-01-01").AddDays(i.D);
		public static explicit operator string(Ideal i) => i.ToString();
		public static explicit operator int(Ideal i) => i.D;
		static void Main(string[] args)
		{
			Console.WriteLine(new Ideal(DateTime.Now));
		}
		public override bool Equals(object o)
		{
			if (o.GetType().Equals(typeof(Ideal))) return this==(Ideal)o;
			if (o.GetType().Equals(typeof(int))) return this == (int)o;
			return true;
		}
		public override int GetHashCode() => base.GetHashCode();
	}
}
