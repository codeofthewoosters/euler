//Problem 1
//If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
//Find the sum of all the multiples of 3 or 5 below 1000.
let sumHavingFactorsOf3or5 limit = 
    {1..limit} 
    |> Seq.filter (fun x ->  x % 3 = 0 || x % 5 = 0) 
    |> Seq.fold (+) 0


//Problem 2 
//Each new term in the Fibonacci sequence is generated by adding the previous two terms. 
//By starting with 1 and 2, the first 10 terms will be:
//1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
//By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
let nextFib (a, b) =
        if b > 4000000 then
            None
        else
            let nextValue = a + b 
            Some(nextValue, (b, nextValue));;

let sumFib = Seq.unfold nextFib (1,2) 
                |> Seq.filter (fun x -> x % 2 = 0)
                |> Seq.fold (+) 2;;


//Problem 3
//The prime factors of 13195 are 5, 7, 13 and 29.
//What is the largest prime factor of the number 600851475143 ?
let rec findPrimeFactors (acc:int64 list) (n:int64) = 
    if (n % 2L = 0L) then
        findPrimeFactors ([2L]) (n/2L)
    else
        let factor = {3L..2L..n} |> Seq.tryFind (fun i -> n % i = 0L)            
        match factor with
        | Some(i) -> 
            let accCont = i::acc
            findPrimeFactors (i::acc) (n/i) 
        | None -> acc

findPrimeFactors [] 600851475143L |> Seq.max



//Problem 4
//A palindromic number reads the same both ways. 
//The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 99.
//Find the largest palindrome made from the product of two 3-digit numbers.
let isPalindrome x = 
    (string x) = new System.String(Array.rev ((string x).ToCharArray()));;

let productsOfThreePalindromesDesc = 
    seq {
        for i in 999..-1..100 do
            for j in 999..-1..100 do
                if isPalindrome(i*j) then yield (i*j) 
    };;

productsOfThreePalindromesDesc |> Seq.max;;



//Problem 5 
//2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
//What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
let prodPrimesBelow20 = 2*3*5*7*11*13*17*19;;
prodPrimesBelow20;;

let hasAllFactors x = 
    {20..-1..2} |> Seq.takeWhile (fun i -> x % i = 0) |> Seq.fold (fun acc a -> a :: acc) [] |> Seq.length = 19;;

let rec checker tocheck = 
    let hasFactors = hasAllFactors tocheck
    match hasFactors with
    | true
        -> tocheck
    | false 
        -> checker (tocheck + prodPrimesBelow20);;

checker prodPrimesBelow20;;


//Problem 6 
//The sum of the squares of the first ten natural numbers is,
//    12 + 22 + ... + 102 = 385
//The square of the sum of the first ten natural numbers is,
//    (1 + 2 + ... + 10)2 = 552 = 3025
//Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025  385 = 2640.
//Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
let squareOfSum = {1..100} |> Seq.fold (fun acc i -> acc + i) 0 |> (fun i -> i*i);;
let sumOfSquare = {1..100} |> Seq.map (fun i -> i*i) |> Seq.sum;;
abs (sumOfSquare - squareOfSum);;



//Problem 7
//By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
//What is the 10 001st prime number?
let rec findNextPrimeUnderNth (primesToX:int64 list) (x:int64) = 
    match primesToX with 
    | [] 
        -> findNextPrimeUnderNth [2L] 3L
    | head :: tail when List.length primesToX < 10001
        -> 
            match primesToX |> Seq.filter (fun i -> (i*i <= x) && (x % i = 0L) ) |> Seq.length = 0 with
            | true 
                -> findNextPrimeUnderNth (x::primesToX) (x + 2L)
            | false 
                -> findNextPrimeUnderNth primesToX (x + 2L)
    | _ 
        -> primesToX |> Seq.max

let findPrimes = findNextPrimeUnderNth [] 0L;;

//Problem 8
//Find the greatest product of five consecutive digits in the 1000-digit number.
(*
73167176531330624919225119674426574742355349194934
96983520312774506326239578318016984801869478851843
85861560789112949495459501737958331952853208805511
12540698747158523863050715693290963295227443043557
66896648950445244523161731856403098711121722383113
62229893423380308135336276614282806444486645238749
30358907296290491560440772390713810515859307960866
70172427121883998797908792274921901699720888093776
65727333001053367881220235421809751254540594752243
52584907711670556013604839586446706324415722155397
53697817977846174064955149290862569321978468622482
83972241375657056057490261407972968652414535100474
82166370484403199890008895243450658541227588666881
16427171479924442928230863465674813919123162824586
17866458359124566529476545682848912883142607690042
24219022671055626321111109370544217506941658960408
07198403850962455444362981230987879927244284909188
84580156166097919133875499200524063689912560717606
05886116467109405077541002256983155200055935729725
71636269561882670428252483600823257530420752963450
*)


let numberString = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";;
let digits = Seq.map (fun x -> int (System.Char.GetNumericValue x)) numberString |> Seq.toArray;;
let prods = 
    {0..((Seq.length digits) - 6)} 
    |> Seq.map (fun i -> i, i+4) 
    |> Seq.fold (fun acc i -> 
                let l = fst i
                let u = snd i
                let prod = digits.[l..u] |> Seq.reduce (fun a b -> a * b)
                prod :: acc
        ) [];;

Seq.max prods;;



//Problem 9 - GETS PRIMITIVES ONLY!!
(*
    A Pythagorean triplet is a set of three natural numbers, a  b  c, for which,

    a2 + b2 = c2
    For example, 32 + 42 = 9 + 16 = 25 = 52.

    There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    Find the product abc.

    a^2 +  b^2 = c^2
*)
//Generate the ternary tree acording the the transforms...
//T1:  a − 2b + 2c     2a − b + 2c     2a − 2b + 3c
//T2:  a + 2b + 2c     2a + b + 2c     2a + 2b + 3c
//T3: −a + 2b + 2c    −2a + b + 2c    −2a + 2b + 3c

let t1 (a,b,c) =
    let a' = a - 2*b + 2*c     
    let b' = 2*a - b + 2*c     
    let c' = 2*a - 2*b + 3*c
    (a',b',c');;

let t2 (a,b,c) = 
    let a' = a + 2*b + 2*c     
    let b' = 2*a + b + 2*c     
    let c' = 2*a + 2*b + 3*c
    (a',b',c');;

let t3 (a,b,c) = 
    let a' = -a + 2*b + 2*c    
    let b' = -2*a + b + 2*c    
    let c' = -2*a + 2*b + 3*c
    (a',b',c');;


let sumAbc (a,b,c) = a+b+c;;

let rec transform pyTuple cont =
    match pyTuple with
    | (x,y,z) when sumAbc pyTuple = 1000
        -> cont pyTuple
    | (x,y,z) when sumAbc pyTuple > 1000
        -> cont pyTuple
    | (x,y,z) when sumAbc pyTuple < 1000 
        ->  
            printfn "Processing tuple t1: %A" (t1 pyTuple)
            printfn "Sum: %d" (sumAbc (t1 pyTuple))
            transform (t1 pyTuple) (fun x -> 
                                            printfn "Processing tuple t2: %A" (t2 pyTuple)
                                            printfn "Sum: %d" (sumAbc (t2 pyTuple))
                                            transform (t2 pyTuple) (
                                                fun x -> 
                                                        printfn "Processing tuple t3: %A" (t3 pyTuple)
                                                        printfn "Sum: %d" (sumAbc (t3 pyTuple))
                                                        transform (t3 pyTuple) cont
                                                        )
                                            )
    | _ 
        ->  printfn "SOMETHING WENT WRONG"
            cont pyTuple;;


let tripleSearch = transform (3,4,5) (fun x -> printfn "The triple is %A" x);;


(*
    Problem 9 Using Brute force....
*)

let isPythagoreanTuple (a,b,c) = (a*a) + (b*b) = (c*c);;
let isAsccendingTuple (a,b,c) = (a < b) && (b < c);;
let sumTuple (a,b,c) = a+b+c;;

let pythagoreanTripple sum = 
    seq {
        for a in 1..500 do
            for b in a..500 do 
                for c in b..1000 do
                    if (isAsccendingTuple (a,b,c)) && (isPythagoreanTuple (a,b,c)) && (sumTuple (a,b,c) = sum) then yield (a,b,c)
    };;

(pythagoreanTripple 1000) |> Seq.take 1;;


//Problem 10
//The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
//Find the sum of all the primes below two million.
let primeGen max = 

    let rec getStrikes strikes x stop n = 
        if x >= stop then 
            strikes
        else 
            let alreadyStruck = strikes |> Seq.tryFind (fun s -> s = x)
            match alreadyStruck with
            | Some(i) 
                ->  getStrikes strikes (x+2) stop n
            | None 
                ->  let newStrikes = strikes @ [(x*x)..x..n]
                    getStrikes newStrikes (x+2) stop n

    let sqrtMax = int (sqrt (float max))
    let strikesToMax = getStrikes [4..2..max] 3 sqrtMax max
    let primes = Set.difference (Set.ofList [2..1..max]) (Set.ofList strikesToMax)
    let total = primes |> Seq.fold (fun acc p -> acc + (int64 p)) 0L
    printfn "Sum of primes up to %d = %d" max total


//Problem 11
(*
In the 2020 grid below, four numbers along a diagonal line have been marked in red.
*)

let gridStr = 
    "08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
	49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
	81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
	52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
	22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
	24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
	32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
	67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
	24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
	21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
	78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
	16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
	86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
	19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
	04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
	88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
	04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
	20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
	20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
	01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";;

(*
The product of these numbers is 26  63  78  14 = 1788696.

What is the greatest product of four adjacent numbers in any direction (up, down, left, right, or diagonally) in the 2020 grid?
*)


let gridToLists (gridStr : string) =
    
    let lines = gridStr.Split [|'\n'|]
    let iDim = lines.Length
    
    let s = [
        for i in [0..(iDim-1)] do
            let iL = (lines.[i].Split[|' '|])
            yield [
                yield! Array.toList (Array.map (fun j -> int j) iL)
            ]
    ]

    printfn "Grid = %A" s
    s;;

let rec transpose = function
    | (_::_)::_ as M -> List.map List.head M :: transpose (List.map List.tail M)
    | _ -> []

//[[1; 2; 3]; [4; 5; 6]; [7; 8; 9]] |> transpose |> printfn "%A"

let grid = gridToLists gridStr;;
grid.[0];;

let processH grd n = 

    let maxI seq adj = 
        Seq.windowed adj seq |> Seq.map (Array.reduce (fun a b -> a * b)) |> Seq.max

    grd |> List.map (fun i -> (maxI i n)) |> List.max;;

let processD grd n rv = 

    let w = grd |> List.length
    let flat = match rv with 
                | true -> grd |> List.rev |> List.collect (fun row -> row)
                | false -> grd |> List.collect (fun row -> row)
    //printfn "Flat: %A" flat    

    let offsets = [ for i in [0..n-1] -> i * (w + 1) ]
    //printfn "Offsets: %A" offsets

    let last = (flat.Length - 1) - List.max offsets
    //printfn "last: %A" last

    let toTest = [0..w..(w*(w-n))] |> List.collect (fun rowStart -> [0..(w-n)] |> List.map (fun col -> col + rowStart))
    //printfn "totest: %A" toTest
    //printfn "totest rev: %A" (List.rev toTest)

    let groups = toTest
                    |> List.collect ( fun i -> [ offsets 
                                                    |> List.map ( fun o -> flat.[i + o] ) 
                                                ]  
                                    )  
    //printfn "Groups: %A" groups
    //printfn "Groups reversed: %A" (List.rev groups)
    let prods = groups |> List.map (fun g -> List.reduce (fun a b -> a * b) g) 
    //printfn "Prods: %A" prods
    prods |> List.max


let maxH = processH grid 4;;
let maxV = processH (transpose grid) 4;;
let diagR = processD grid 4 false;;
let diagL = processD grid 4 true;;



//Problem 12
(*
The sequence of triangle numbers is generated by adding the natural numbers. So the 7th triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. The first ten terms would be:

1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

Let us list the factors of the first seven triangle numbers:

 1: 1
 3: 1,3
 6: 1,2,3,6
10: 1,2,5,10
15: 1,3,5,15
21: 1,3,7,21
28: 1,2,4,7,14,28

We can see that 28 is the first triangle number to have over five divisors.

What is the value of the first triangle number to have over five hundred divisors?
*)

//(n, n + 1) -> (f(n), (n + 1, n + 2))

let divisorFunction (primes:int64 seq) = 
    
    if Seq.isEmpty primes then 
        1
    else 
        primes 
            |> Seq.groupBy (fun p -> p) 
            |> Seq.map (fun g -> List.length (Seq.toList (snd g)) )
            |> Seq.fold (fun acc c -> acc * (c+1)) 1;;


let primesOf (n:int64) = 

    let rec byTrial (n:int64) (acc:int64 list) = 
        if (n % 2L = 0L) then
            byTrial (n/2L) (2L :: acc)
        else
            let factor = {3L..2L..n} |> Seq.tryFind (fun i -> n % i = 0L)            
            match factor with
            | Some(i) -> 
                byTrial (n/i) (i::acc)
            | None -> acc

    if n = 0L then 
        []
    else 
        byTrial n [];;

//t = n * (n+1) / 2 = (n^2 + n) / 2 -> .....  n^2 / + n / 2 - t = 0 ...
let nextTriangle (a, b) =
    let triangle = a * b / 2
    Some((a, triangle), (b, b + 1));;

Seq.unfold nextTriangle (1,2) 
    |> Seq.map (fun (i,t) -> (t, primesOf (int64 t)) )
    |> Seq.map (fun (x,y) -> (x, y, (divisorFunction y)))
    |> Seq.filter (fun (x,y,z) -> z > 500)
    |> Seq.take 1
    |> Seq.iter (fun (x,y,z) -> printfn "Triangle: %A, Primes: %A, Factors: %A" x y z);;





//Problem 13 
//Work out the first ten digits of the sum of the following one-hundred 50-digit numbers.

let digitStr = "37107287533902102798797998220837590246510135740250
46376937677490009712648124896970078050417018260538
74324986199524741059474233309513058123726617309629
91942213363574161572522430563301811072406154908250
23067588207539346171171980310421047513778063246676
89261670696623633820136378418383684178734361726757
28112879812849979408065481931592621691275889832738
44274228917432520321923589422876796487670272189318
47451445736001306439091167216856844588711603153276
70386486105843025439939619828917593665686757934951
62176457141856560629502157223196586755079324193331
64906352462741904929101432445813822663347944758178
92575867718337217661963751590579239728245598838407
58203565325359399008402633568948830189458628227828
80181199384826282014278194139940567587151170094390
35398664372827112653829987240784473053190104293586
86515506006295864861532075273371959191420517255829
71693888707715466499115593487603532921714970056938
54370070576826684624621495650076471787294438377604
53282654108756828443191190634694037855217779295145
36123272525000296071075082563815656710885258350721
45876576172410976447339110607218265236877223636045
17423706905851860660448207621209813287860733969412
81142660418086830619328460811191061556940512689692
51934325451728388641918047049293215058642563049483
62467221648435076201727918039944693004732956340691
15732444386908125794514089057706229429197107928209
55037687525678773091862540744969844508330393682126
18336384825330154686196124348767681297534375946515
80386287592878490201521685554828717201219257766954
78182833757993103614740356856449095527097864797581
16726320100436897842553539920931837441497806860984
48403098129077791799088218795327364475675590848030
87086987551392711854517078544161852424320693150332
59959406895756536782107074926966537676326235447210
69793950679652694742597709739166693763042633987085
41052684708299085211399427365734116182760315001271
65378607361501080857009149939512557028198746004375
35829035317434717326932123578154982629742552737307
94953759765105305946966067683156574377167401875275
88902802571733229619176668713819931811048770190271
25267680276078003013678680992525463401061632866526
36270218540497705585629946580636237993140746255962
24074486908231174977792365466257246923322810917141
91430288197103288597806669760892938638285025333403
34413065578016127815921815005561868836468420090470
23053081172816430487623791969842487255036638784583
11487696932154902810424020138335124462181441773470
63783299490636259666498587618221225225512486764533
67720186971698544312419572409913959008952310058822
95548255300263520781532296796249481641953868218774
76085327132285723110424803456124867697064507995236
37774242535411291684276865538926205024910326572967
23701913275725675285653248258265463092207058596522
29798860272258331913126375147341994889534765745501
18495701454879288984856827726077713721403798879715
38298203783031473527721580348144513491373226651381
34829543829199918180278916522431027392251122869539
40957953066405232632538044100059654939159879593635
29746152185502371307642255121183693803580388584903
41698116222072977186158236678424689157993532961922
62467957194401269043877107275048102390895523597457
23189706772547915061505504953922979530901129967519
86188088225875314529584099251203829009407770775672
11306739708304724483816533873502340845647058077308
82959174767140363198008187129011875491310547126581
97623331044818386269515456334926366572897563400500
42846280183517070527831839425882145521227251250327
55121603546981200581762165212827652751691296897789
32238195734329339946437501907836945765883352399886
75506164965184775180738168837861091527357929701337
62177842752192623401942399639168044983993173312731
32924185707147349566916674687634660915035914677504
99518671430235219628894890102423325116913619626622
73267460800591547471830798392868535206946944540724
76841822524674417161514036427982273348055556214818
97142617910342598647204516893989422179826088076852
87783646182799346313767754307809363333018982642090
10848802521674670883215120185883543223812876952786
71329612474782464538636993009049310363619763878039
62184073572399794223406235393808339651327408011116
66627891981488087797941876876144230030984490851411
60661826293682836764744779239180335110989069790714
85786944089552990653640447425576083659976645795096
66024396409905389607120198219976047599490197230297
64913982680032973156037120041377903785566085089252
16730939319872750275468906903707539413042652315011
94809377245048795150954100921645863754710598436791
78639167021187492431995700641917969777599028300699
15368713711936614952811305876380278410754449733078
40789923115535562561142322423255033685442488917353
44889911501440648020369068063960672322193204149535
41503128880339536053299340368006977710650566631954
81234880673210146739058568557934581403627822703280
82616570773948327592232845941706525094512325230608
22918802058777319719839450180888072429661980811197
77158542502016545090413245809786882778948721859617
72107838435069186155435662884062257473692284509516
20849603980134001723930671666823555245252804609722
53503534226472524250874054075591789781264330331690"



let numbersToList (str:string) bs = 
    let lines = str.Split [|'\n'|] |> Array.toList
    let len = lines.[0].Length
    let starts = [(len-bs)..(-bs)..0]

    starts |> List.map (fun s -> lines 
                                    |> List.map (fun l -> l.Substring(s, bs)) );;

let strData = numbersToList digitStr 10;;


let addBaseStrings init (ls:string list)= 

    let rec addTwoStrings (str1:string) (str2:string) units pwrs = 
        let l = str2.Length
        let sum = (int64 str1) + (int64 str2) + (units)
        printfn "Str1:%A Str2:%A Units:%A Powers:%A Total:%A" str1 str2 units pwrs sum

        let newUnits = 0L
        let newPwr = (sum / (pown 10L l)) + pwrs
        let newStr = (sum % (pown 10L l)).ToString("D" + (string l))
        printfn "addTwoStrings: Units=%d Pwr=%d NewStr=%s" newUnits newPwr newStr
        (newUnits, newPwr, newStr)

    let bins = ls.Length
    let bs = ls.[bins-1].Length

    let (units, pwrs, finalStr) = ls |> List.fold (fun (units, pwrs, currStr) (nextStr) -> 
                                                    addTwoStrings currStr nextStr units pwrs) (init, 0L, "000")
    (pwrs, finalStr);;

addBaseStrings 25L ["999";"999"];;

let addNumberArrays strAdder data = 

    let rec addStringSets strAdder crry res data =
        match data with
        | [] 
            -> (crry, res)
        | hd :: tl 
            ->  let (newCarry, newBaseStr) = strAdder crry hd
                //printfn "Res to date: %A" res
                addStringSets strAdder newCarry (newBaseStr::res) tl

    let (finalCarry, resInReverse) = addStringSets strAdder 0L [] data
    printfn "FinalCarry: %A" finalCarry
    resInReverse |> List.iter (fun i -> printfn "%A" i)
    resInReverse |> List.fold (fun acc s -> acc + s) (string finalCarry)


let x = addNumberArrays addBaseStrings strData;;
x;;


//The following iterative sequence is defined for the set of positive integers:
//
//n → n/2 (n is even)
//n → 3n + 1 (n is odd)
//
//Using the rule above and starting with 13, we generate the following sequence:
//
//13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
//It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. 
//Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
//
//Which starting number, under one million, produces the longest chain?
//
//NOTE: Once the chain starts the terms are allowed to go above one million.

(* 
  Notes - The approach here will be to encode the algorithm naively, but to memoize the calculation. 
          The assumption is that there will be a lot of sharing in the calculation graph if the hypothesis
          holds true that the numbers tend to 1 in all cases and don't degenerate. 
*)                          
#nowarn "40"                  
let rec recurrence = 
    (fun c ->  
             (fun n -> 
                   match n with 
                   | 1 -> c + 1
                   | _ -> 
                       match n % 2 with 
                       | 0 -> recurrence (c + 1) (n / 2)
                       | _ -> recurrence (c + 1) (3 * n + 1)
                    )
              )
   
seq {for i in 1..1000000 do yield (i, recurrence 0 i)} |> Seq.maxBy snd 
                                        
// can we memoize? - we need to make this tail recursive!
// we need to use some combination of memoization and cps to move N calls from stack to heap
/// On Mono this still blows the stack, which makes me think I am not doing something right, or mono is buggy.
// see - http://stackoverflow.com/questions/3459422/combine-memoization-and-tail-recursion
let memoize work =
    let cache = System.Collections.Generic.Dictionary<_,_>()
    let rec newFunc key cont =  // must make tailcalls to k
        match cache.TryGetValue(key) with
        | true, r -> cont r // unwind
        | _ -> 
            work key (fun r ->
                        cache.Add(key,r)
                        cont r) newFunc
    cache, (fun n -> newFunc n id)
   
let rec memRecur n state memWork = 
    match n with 
        | 1 -> state 1
        | _ -> 
           match n % 2 with 
           | 0 -> memWork (n / 2) (fun k0 -> 
                                       let k = 1 + k0; 
                                       state k)
           | _ -> memWork (3 * n + 1) (fun k0 -> 
                                       let k = 1 + k0; 
                                       state k)
 
let cache, seqLen = memoize memRecur                                                          
let mutable maxi = 0
let mutable maxchain = 0
let mutable i = 100000
while i < 1000001 do
    let chn = seqLen i
    printfn "Seed value %A has chain length %A" i chn
    if chn > maxchain 
    then maxchain <- chn
         maxi <- i
    i <- i + 1
printfn "Seed value %A has chain length %A" maxi maxchain 



(*
Problem 15
Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
there are exactly 6 routes to the bottom right corner.
How many such routes are there through a 20×20 grid?
*)

// blows the stack
let rec getRoutes (mx,my) (x,y) cont = 
    match x,y with 
    | x, y when x = mx && y = my -> cont 1
    | x, _ when x = mx -> getRoutes (mx,my) (x,y+1) cont
    | _, y when y = my -> getRoutes (mx,my) (x+1,y) cont
    | _, _ -> getRoutes (mx,my) (x+1,y) (fun lacc -> lacc + getRoutes (mx,my) (x,y+1) cont)
    
getRoutes (4,4) (0,0) id

// we can use pascals triangle here ... we want the max (middle) value of the 20th row....
#nowarn "40"
let rec pascal = seq { 
    yield [1L];
    for aLine in pascal -> 
            let newLine = 
                aLine |> Seq.pairwise |> Seq.map (fun (x,y) -> (x+y)) |> Seq.toList
            (1L::newLine) @ [1L] 
    }
            
pascal |> Seq.nth 40 |> Seq.max














    
 