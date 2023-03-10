package ttt;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashMap;

import javax.swing.JFrame;

public class TTTEngine {
		
	static String[][] grid = new String[3][3];
	static HashMap<String, Integer> scores = new HashMap<>();
	public static void main(String[] args)  {
		scores.put("x", 10);scores.put("o", -10);scores.put("t", 0);scores.put(".", 10);
		//receive
		try {
		JFrame fr = new JFrame("engine's running");
		fr.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		fr.setVisible(true);
		fr.setState(JFrame.ICONIFIED);
        ServerSocket serverSocket = new ServerSocket(8080, 10);
        fr.setTitle("server started (dont close this)");
        System.out.println("server started");
        Socket socket = serverSocket.accept();
        InputStream is = socket.getInputStream();
        OutputStream os = socket.getOutputStream();
        fr.setTitle("connected");
        System.out.println("connected");
        fr.setVisible(false);
        fr.dispose();
        while(true) {
        	the(is,os);
        }
       
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	public static void the(InputStream is, OutputStream os) {
		try {
		 byte[] receivedBytes = new byte[14];
	        is.read(receivedBytes, 0, 14);
	        String received = new String(receivedBytes, 0, 14);
	        if(received.isBlank() || received.isEmpty()) 
	        	System.exit(2);
	        System.out.println("r: "+received);
	        for(int i = 0; i<3; i++) {
	        	for(int j = 0; j<3; j++) {
	        		grid[i][j] = received.split("/")[i].split("")[j];
	        	}
	        }
	        
	        String winner = checkWinner("x", grid);
	        
	        //send
	        if(winner!=".") 
	        	os.write(("won:"+winner).getBytes());
	        else {
	        	os.write(bestMove().getBytes());
	        }
	        
		}
		catch(Exception e) {
			if(e.getClass().getName().equals("java.lang.ArrayIndexOutOfBoundsException")) {
				System.out.println("lost connection");
				System.exit(1);
			}
			e.printStackTrace();
		}
	}
	public static String checkWinner(String turn, String[][] grid) {
		//horizontal
		for(int i = 0; i<3; i++) {	
			if(grid[i][0].equals(turn) && grid[i][1].equals(turn) && grid[i][2].equals(turn)) 
				return turn;
		}
		//vertical
		for(int i = 0; i<3; i++) {	
			if(grid[0][i].equals(turn) && grid[1][i].equals(turn) && grid[2][i].equals(turn))
				return turn;
		}
		//diagonals
		if(grid[0][0].equals(turn) && grid[1][1].equals(turn) && grid[2][2].equals(turn))
			return turn;
		if(grid[0][2].equals(turn) && grid[1][1].equals(turn) && grid[2][0].equals(turn))
			return turn;
		
		//returns
		if(turn.equals("x"))
			return checkWinner("o", grid);
		if(turn.equals("o")) {
			if(checkTie()) 
				return "t";
			return ".";
		}
		System.out.println("neither x nor o");
		return ".";
		
	}

	public static boolean checkTie() {
		for(int i = 0; i<3; i++)
			for(int j = 0; j<3; j++)
				if(grid[i][j].equals("."))
					return false;
		return true;
	}

	public static String bestMove() {
		int score = 0, bestScore = Integer.MAX_VALUE,x=0,y=0;
		
		for(int i = 0; i<3; i++) {
			for(int j = 0; j<3; j++) {
				if(grid[i][j].equals(".")) {
					grid[i][j]="o";
					score = minimax(true, Integer.MIN_VALUE, Integer.MAX_VALUE);
					grid[i][j]=".";
					if(score<bestScore) {
						bestScore=score;
						x=i;y=j;
					}
					
				}
			}
		}
		grid[x][y]="o";
		String winner = checkWinner("x", grid);
        if(winner!=".") 
        	return "won:"+winner+"$"+gridToString(grid);
        else {
    		return gridToString(grid);
        }
	}
	
	public static int minimax(boolean maxi, int alpha, int beta) {
		if(!checkWinner("x", grid).equals(".")) 
			return scores.get(checkWinner("x", grid));
		
		if(maxi) {
			int score=0, bestScore = Integer.MIN_VALUE;
			p: for(int i = 0; i<3; i++) {
				for(int j = 0; j<3; j++) {
					if(grid[i][j].equals(".")) {
						grid[i][j]="x";
						score = minimax(false, alpha, beta);
						grid[i][j]=".";
						bestScore=Math.max(score, bestScore);
						alpha = Math.max(alpha, bestScore);
						if(beta<=alpha)
							break p;
					}
				}
			}
			
			return bestScore;
		}
		else {
			int score=0, bestScore = Integer.MAX_VALUE;
			p: for(int i = 0; i<3; i++) {
				for(int j = 0; j<3; j++) {
					if(grid[i][j].equals(".")) {
						grid[i][j]="o";
						score = minimax(true, alpha, beta);
						grid[i][j]=".";
						bestScore=Math.min(score, bestScore);
						beta = Math.min(beta, bestScore);
						if(beta<=alpha)
							break p;
					}
				}
			}
			
			return bestScore;
		}
		
	}
	
	public static String gridToString(String[][] grid) {
		String st = "";
		for(int i = 0; i<3; i++) {
			for(int j = 0; j<3; j++) {
				st+=grid[i][j];
			}
			st+="/";
		}
		
		return st;
	}

}



