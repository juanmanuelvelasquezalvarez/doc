<?php
//If the year has or not 1 week more
function days(int $y) {
	return $y==round(round($y*1.242189/7)*7/1.242189)?371:364;
}
class Ideal {
	private $D;//Days since the change of millennium initiating in 0.
	private $y=2000;//Year initiating in 2000
	private $d=6;//Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
	//The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
	//Gregorian, year and day
	//If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
	public function __construct($g, $y=2000, $d=7) {
		try { 
			$this->addDays(($g-(int)date("U",mktime(0,0,0,1,1,2000)))/86400);
		}catch(Exception $e) {
			$this->addYears($y-2000);
			$this->addDays($d-7);
		}
	}
	public function addDays(int $n) {
		$this->d+=$n;
		if($n>0) {
			$N=days($this->y);
			while($this->d>=$N) {
				$this->d-=$N;
				$N=days(++$this->y);
			}
		}
		else while($this->d<0) $this->d+=days(--$this->y);
		$this->D+=$n;
	}
	public function addYears(int $n) {
		$a=$this->y+$n;
		if($this->d>363) if(days($a)==364) {
			$this->d-=7;
			$this->D-=7;
		}
		if($n>0) while($this->y<$a) $this->D+=days($this->y++);
		if($n<0) while($this->y>$a) $this->D-=days(--$this->y);
	}
	//Choose among year, month, week, day of the year/month/week.
	//Week and month start at 0 internally and here 1 is added.
	public function __get($f) {
		switch($f) {
			case 'y': return $this->y;
			case 'm': return floor($this->d/28)+1;
			case 'w': return floor($this->d/7)+1;
			case 'd': return $this->d;
			case 'D': return $this->D;
			case 'dm': return $this->d%28+1;
			case 'dw': return $this->d%7+1;
			//Ideal calendar to Gregorian.
			case 'g': return date_add(date_create("2000-01-01"),date_interval_create_from_date_string($this->D.' days'));
		}
		return 0;
	}
}
$i=new Ideal((int)date("U"));
echo date_format($i->g,"Y/m/d").'<br>'.$i->dm." ".$i->m." ".$i->y.'<br>';
