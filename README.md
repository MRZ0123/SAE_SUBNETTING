# SAE_SUBNETTING
Our repository for the second SAE project work - a very small subnetting program

## Goals
Our goal for this project was to make it run as efficiently as possible.

Therefore we ran multiple benchmarks with 3 different ways of storing the IP address and subnet mask.

|      | int[]      | byte[]    | uint      |
|------|------------|-----------|-----------|
| avg. | 37.892920s | 9.027391s | 8.274380s |

## Benchmarking methology
Our benchmark ran the subnet mask validation part of the code since it was the part with the most cpu cycles spent.

1 run = testing all IP addresses from 255.255.**0**.0 to 255.255.**255**.255
(the bold sections were marked as hex so instead of cycling from 0 to 9 per digit it cycled from 0 to F per digit)
> ex. <br />
> (test number 75776) <br />
> IP address:
> 255.255.128.0<br />(256 tests later) <br />IP address:
> 255.255.129.0<br />(256 tests later) <br />IP address:
> 255.255.12A.0

=> all in all there are 152832 tests per run

for each benchmark we ran every way of storing the IP address and subnetmask 3 times and averaged the execution time those three runs

last but not least we repeated every benchmark 5 times and put the average of those five benchmarks into the comparison table above

## Conclusion
Therefore we ended up using a UInt32 as storage for the IP address and subnet mask to be able to do the most efficient calculations.