/*Milliseconds from the counting (1st second of 1970 Greenwich)
to the glorious change of millennium: 01/01/2000.
In the class Date, month starts at 0*/
var m=new Date(2000,0,1,0,0,0,0).getTime();
//If the year has or not 1 week more
function days(y) {
	return y==Math.round(Math.round(y*1.242189/7)*7/1.242189)?371:364;
}
class Ideal {
	//The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
	constructor(g) {//From Gregorian to ideal calendar
		this.D=0;//Days since the change of millennium initiating in 0.
		this.y=2000;//Year initiating in 2000
		this.d=6;//Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
		if(g==null | (typeof g)!="number" & (typeof g)!="Date") g=new Date().getTime()
		this.addDays((((typeof g)=="Date"?g.getTime():g)-m)/86400000);
	}
	//Year and day. If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
	set(y,d) {
		addYears(y-this.y);addDays(d-this.d-1);
	}
	addDays(n) {
		this.d+=n;
		if(n>0) {
			var N=days(this.y);
			while(this.d>=N) {
				this.d-=N;
				N=days(++this.y);
			}
		}
		else while(this.d<0) this.d+=days(--this.y);
		this.D+=n;
	}
	addYears(n) {
		var a=this.y+n;
		if(d>363) if(days(a)==364) {
			d-=7;D-=7;
		}
		if(n>0) while(y<a) this.D+=days(this.y++);
		if(n<0) while(y>a) this.D-=days(--this.y);
	}
	//Choose among year, month, week, day of the year/month/week.
	//Day, week and month start at 0 internally and here 1 is added.
	get(f) {
		return f<1|f>7?0:f==1?y:f==7?D:Math.floor(f==2?this.d/28:f==3?this.d/7:f==4?this.d:f==5?this.d%28:this.d%7)+1;
	}
	//Day of the year, day (1 to 28) and month or day (1 to 7) and week and the year
	string(f) {
		return ((typeof f)!="boolean"?this.d+1:this.get(f?5:6)+" "+this.get(f?2:3))+" "+this.y;
	}
	//Ideal calendar to Gregorian from milliseconds.
	gregorian() {
		return new Date(D*86400000+m);
	}
}
document.getElementsByTagName("p")[0].innerHTML = new Ideal().string(true);//Now
