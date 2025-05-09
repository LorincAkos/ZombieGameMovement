from collections import deque
import heapq

def parse_graph_file(file_path):
    graphs = {}
    current_graph = None
    mode = None

    with open(file_path, 'r') as f:
        for line in f:
            line = line.strip()
            if not line or line.startswith('#'):  # Skip empty lines and comments
                continue
            
            if line.startswith("graph"):
                current_graph = line.split()[1]
                graphs[current_graph] = {"nodes": [], "edges": [], "heuristics": {}}
                mode = None
            
            elif line.startswith("nodes"):
                mode = "nodes"
                graphs[current_graph]["nodes"] = line.split()[1:]
            
            elif line.startswith("edges"):
                mode = "edges"
            
            elif line.startswith("heuristic-start"):
                mode = "heuristics"
            
            elif line.startswith("heuristic-end"):
                mode = None
            
            elif mode == "edges" and current_graph:
                parts = line.split()
                if len(parts) == 2:
                    node1, node2 = parts
                    graphs[current_graph]["edges"].append((node1, node2, None))  # No weight
                elif len(parts) == 3:
                    node1, node2, weight = parts
                    graphs[current_graph]["edges"].append((node1, node2, float(weight)))
            
            elif mode == "heuristics" and current_graph:
                heuristic_line = line.split()
                node = heuristic_line[0]
                heuristics = {pair.split('-')[0]: float(pair.split('-')[1]) for pair in heuristic_line[1:]}
                graphs[current_graph]["heuristics"][node] = heuristics

    return graphs

def dfs(graph_data, start_node, goal_node=None):
    """
    Perform Depth-First Search on the given graph.
    
    :param graph_data: Dictionary containing the graph's nodes, edges, and other properties.
    :param start_node: Node to start the search from.
    :param goal_node: (Optional) Goal node to stop the search when reached.
    :return: List representing the path taken by DFS.
    """
    # Create an adjacency list for the graph
    adjacency_list = {node: [] for node in graph_data["nodes"]}
    for edge in graph_data["edges"]:
        node1, node2, _ = edge  # Ignore edge weights
        adjacency_list[node1].append(node2)
        adjacency_list[node2].append(node1)  # Assuming undirected graph

    visited = set()
    path = []

    def dfs_recursive(node):
        if node in visited:
            return False
        visited.add(node)
        path.append(node)

        # Stop if goal_node is reached
        if node == goal_node:
            return True

        for neighbor in adjacency_list[node]:
            if dfs_recursive(neighbor):
                return True

        # Backtrack if no valid path is found
        path.pop()
        return False

    dfs_recursive(start_node)
    return path

def bfs(graph_data, start_node, goal_node=None):
    """
    Perform Breadth-First Search on the given graph.
    
    :param graph_data: Dictionary containing the graph's nodes, edges, and other properties.
    :param start_node: Node to start the search from.
    :param goal_node: (Optional) Goal node to stop the search when reached.
    :return: List representing the path taken by BFS.
    """
    # Create an adjacency list for the graph
    adjacency_list = {node: [] for node in graph_data["nodes"]}
    for edge in graph_data["edges"]:
        node1, node2, _ = edge  # Ignore edge weights
        adjacency_list[node1].append(node2)
        adjacency_list[node2].append(node1)  # Assuming undirected graph

    visited = set()
    queue = deque([(start_node, [start_node])])  # Store nodes with the current path

    while queue:
        current_node, path = queue.popleft()

        if current_node in visited:
            continue
        visited.add(current_node)

        # If the goal is reached, return the path
        if goal_node and current_node == goal_node:
            return path

        # Add neighbors to the queue
        for neighbor in adjacency_list[current_node]:
            if neighbor not in visited:
                queue.append((neighbor, path + [neighbor]))

    return []  # Return an empty path if the goal is not reachable

def hill_climbing(graph_data, start_node, goal_node, heuristic_key):
    """
    Perform Hill Climbing Search on the given graph.

    :param graph_data: Dictionary containing the graph's nodes, edges, and heuristics.
    :param start_node: Node to start the search from.
    :param goal_node: Node to reach.
    :param heuristic_key: Key for selecting the heuristic (e.g., 'G' for the goal node).
    :return: List representing the path taken by Hill Climbing.
    """
    # Create an adjacency list for the graph
    adjacency_list = {node: [] for node in graph_data["nodes"]}
    for edge in graph_data["edges"]:
        node1, node2, _ = edge  # Ignore edge weights
        adjacency_list[node1].append(node2)
        adjacency_list[node2].append(node1)  # Assuming undirected graph

    path = [start_node]
    visited = set()  # Track visited nodes to prevent cycles
    current_node = start_node

    while current_node != goal_node:
        neighbors = adjacency_list[current_node]

        if not neighbors:
            # Dead-end reached; terminate search
            return path

        # Access heuristics for the goal node
        goal_heuristics = graph_data["heuristics"].get(goal_node, {})

        # Filter neighbors with valid heuristic values and not visited
        valid_neighbors = [
            neighbor for neighbor in neighbors
            if neighbor in goal_heuristics and neighbor not in visited
        ]


        if not valid_neighbors:
            # No valid neighbors to proceed
            break

        # Select the neighbor with the best (lowest) heuristic value
        next_node = min(
            valid_neighbors,
            key=lambda node: goal_heuristics[node]
        )


        # Add the current node to the visited set
        visited.add(current_node)

        # Append the next node to the path
        path.append(next_node)
        current_node = next_node

    return path

def best_first_search(graph_data, start_node, goal_node, heuristic_key):
    """
    Perform Best First Search on the given graph.

    :param graph_data: Dictionary containing the graph's nodes, edges, and heuristics.
    :param start_node: Node to start the search from.
    :param goal_node: Node to reach.
    :param heuristic_key: Key for selecting the heuristic (e.g., 'G' for the goal node).
    :return: List representing the path taken by Best First Search.
    """
    # Create an adjacency list for the graph
    adjacency_list = {node: [] for node in graph_data["nodes"]}
    for edge in graph_data["edges"]:
        node1, node2, _ = edge  # Ignore edge weights
        adjacency_list[node1].append(node2)
        adjacency_list[node2].append(node1)  # Assuming undirected graph


    # Min-heap to always expand the node with the lowest heuristic value
    frontier = []
    heapq.heappush(frontier, (graph_data["heuristics"][goal_node][start_node], start_node))

    # Dictionary to store the path taken
    came_from = {start_node: None}

    while frontier:
        _, current_node = heapq.heappop(frontier)

        if current_node == goal_node:
            # Reconstruct the path from start to goal
            path = []
            while current_node is not None:
                path.append(current_node)
                current_node = came_from[current_node]
            path.reverse()  # Reverse the path to get it from start to goal
            return path

        # Explore neighbors
        for neighbor in adjacency_list[current_node]:
            if neighbor not in came_from:
                # Calculate the heuristic value for the neighbor
                priority = graph_data["heuristics"][goal_node].get(neighbor, float('inf'))

                heapq.heappush(frontier, (priority, neighbor))
                came_from[neighbor] = current_node

    return []  # Return empty path if no solution found

def beam_search(graph_data, start_node, goal_node, heuristic_key, beam_width):
    # Extract graph details
    edges = graph_data['edges']
    heuristics = graph_data['heuristics'].get(goal_node, {})

    # Convert edges to adjacency list
    adjacency_list = {}
    for node1, node2, cost in edges:
        if node1 not in adjacency_list:
            adjacency_list[node1] = []
        if node2 not in adjacency_list:
            adjacency_list[node2] = []
        adjacency_list[node1].append((node2, cost))
        adjacency_list[node2].append((node1, cost))  # Assuming undirected graph

    # Priority queue for beam search
    beam = [(heuristics.get(start_node, float('inf')), 0, [start_node])]

    while beam:
        next_beam = []
        for _, g_cost, current_path in beam:
            current_node = current_path[-1]

            # If the goal node is reached, return the path and cost
            if current_node == goal_node:
                return current_path, g_cost

            # Explore neighbors
            for neighbor, edge_cost in adjacency_list.get(current_node, []):
                if neighbor not in current_path:  # Avoid revisiting nodes
                    new_g_cost = g_cost + edge_cost
                    h_cost = heuristics.get(neighbor, float('inf'))
                    next_beam.append((h_cost, new_g_cost, current_path + [neighbor]))

        # Keep the best `beam_width` nodes based on the heuristic
        next_beam.sort(key=lambda x: x[0])  # Sort by heuristic cost
        beam = next_beam[:beam_width]

    return None, float('inf')  # No path found

def branch_and_bound(graph_data, start_node, goal_node):
    # Convert edges to adjacency list with costs
    edges = graph_data['edges']
    adjacency_list = {}
    for node1, node2, cost in edges:
        if node1 not in adjacency_list:
            adjacency_list[node1] = []
        if node2 not in adjacency_list:
            adjacency_list[node2] = []
        adjacency_list[node1].append((node2, cost))
        adjacency_list[node2].append((node1, cost))  # Assuming undirected graph

    # Priority queue: (path_cost, current_path)
    pq = []
    heapq.heappush(pq, (0, [start_node]))
    
    visited = set()

    while pq:
        # Get the node with the least path cost
        current_cost, current_path = heapq.heappop(pq)
        current_node = current_path[-1]

        # If we've already visited this node, skip it
        if current_node in visited:
            continue

        # Mark the node as visited
        visited.add(current_node)

        # Goal check
        if current_node == goal_node:
            return current_path, current_cost

        # Expand neighbors
        for neighbor, edge_cost in adjacency_list.get(current_node, []):
            if neighbor not in visited:
                total_cost = current_cost + edge_cost
                new_path = current_path + [neighbor]
                heapq.heappush(pq, (total_cost, new_path))

    return None, float('inf')  # No path found

def branch_and_bound_extended(graph_data, start_node, goal_node):
    # Extract edges and convert to adjacency list with costs
    edges = graph_data['edges']
    adjacency_list = {}
    for node1, node2, cost in edges:
        if node1 not in adjacency_list:
            adjacency_list[node1] = []
        if node2 not in adjacency_list:
            adjacency_list[node2] = []
        adjacency_list[node1].append((node2, cost))
        adjacency_list[node2].append((node1, cost))  # Assuming undirected graph

    # Priority queue: (path_cost, current_path)
    pq = []
    heapq.heappush(pq, (0, [start_node]))
    
    # Extended set to store visited nodes with the minimum cost to reach them
    extended_set = {}

    while pq:
        # Get the node with the least path cost
        current_cost, current_path = heapq.heappop(pq)
        current_node = current_path[-1]

        # If we've reached the goal, return the path and its cost
        if current_node == goal_node:
            return current_path, current_cost

        # If the current node is already visited with a lower cost, skip it
        if current_node in extended_set and extended_set[current_node] <= current_cost:
            continue

        # Update the extended set with the current node and its cost
        extended_set[current_node] = current_cost

        # Expand neighbors
        for neighbor, edge_cost in adjacency_list.get(current_node, []):
            total_cost = current_cost + edge_cost
            new_path = current_path + [neighbor]
            # Add to the priority queue for further exploration
            heapq.heappush(pq, (total_cost, new_path))

    return None, float('inf')  # No path found

def branch_and_bound_heuristic(graph_data, start_node, goal_node, heuristic_key):
    # Extract graph details
    edges = graph_data['edges']
    heuristics = graph_data['heuristics'].get(goal_node, {})
    
    # Convert edges to an adjacency list
    adjacency_list = {}
    for node1, node2, cost in edges:
        if node1 not in adjacency_list:
            adjacency_list[node1] = []
        if node2 not in adjacency_list:
            adjacency_list[node2] = []
        adjacency_list[node1].append((node2, cost))
        adjacency_list[node2].append((node1, cost))  # Assuming undirected graph

    # Priority queue: (estimated_total_cost, path_cost, current_path)
    pq = []
    heapq.heappush(pq, (heuristics.get(start_node, float('inf')), 0, [start_node]))
    
    # Extended set for visited nodes
    extended_set = {}

    while pq:
        # Get the path with the smallest estimated cost
        estimated_total_cost, path_cost, current_path = heapq.heappop(pq)
        current_node = current_path[-1]

        # If the goal is reached, return the path and its cost
        if current_node == goal_node:
            return current_path, path_cost

        # Skip if already visited with a cheaper cost
        if current_node in extended_set and extended_set[current_node] <= path_cost:
            continue

        # Mark the current node with the current cost
        extended_set[current_node] = path_cost

        # Expand neighbors
        for neighbor, edge_cost in adjacency_list.get(current_node, []):
            new_cost = path_cost + edge_cost
            heuristic_cost = heuristics.get(neighbor, float('inf'))
            estimated_cost = new_cost + heuristic_cost
            new_path = current_path + [neighbor]
            # Push into the priority queue
            heapq.heappush(pq, (estimated_cost, new_cost, new_path))

    return None, float('inf')  # No path found

def a_star_search(graph_data, start_node, goal_node, heuristic_key):
    # Extract graph details
    edges = graph_data['edges']
    heuristics = graph_data['heuristics'].get(goal_node, {})
    
    # Convert edges to adjacency list
    adjacency_list = {}
    for node1, node2, cost in edges:
        if node1 not in adjacency_list:
            adjacency_list[node1] = []
        if node2 not in adjacency_list:
            adjacency_list[node2] = []
        adjacency_list[node1].append((node2, cost))
        adjacency_list[node2].append((node1, cost))  # Assuming undirected graph

    # Priority queue: (f_cost, g_cost, current_path)
    open_list = []
    heapq.heappush(open_list, (heuristics.get(start_node, float('inf')), 0, [start_node]))

    # Closed set for visited nodes
    closed_set = {}

    while open_list:
        # Get the node with the smallest f_cost
        f_cost, g_cost, current_path = heapq.heappop(open_list)
        current_node = current_path[-1]

        # If goal is reached, return the path and cost
        if current_node == goal_node:
            return current_path, g_cost

        # Skip if already visited with a smaller cost
        if current_node in closed_set and closed_set[current_node] <= g_cost:
            continue

        # Mark node as visited
        closed_set[current_node] = g_cost

        # Explore neighbors
        for neighbor, edge_cost in adjacency_list.get(current_node, []):
            new_g_cost = g_cost + edge_cost
            h_cost = heuristics.get(neighbor, float('inf'))
            f_cost = new_g_cost + h_cost
            heapq.heappush(open_list, (f_cost, new_g_cost, current_path + [neighbor]))

    return None, float('inf')  # No path found

# Usage
file_path = "graph.txt"  # Replace with the actual path to your file
graph_data = parse_graph_file(file_path)

# Usage Example
# Assuming we have already parsed the file into `graph_data`
graph_name = "GRAPH_3"
start = "s"
goal = "g"
heuristic_key = goal
beam_width = 3  # Increase beam width for better exploration
#print(graph_data)  # Check the structure of graph_data
#print(graph_data[graph_name])

path = dfs(graph_data[graph_name], start, goal)
print(f"DFS path from {start} to {goal}: {path}")

path = bfs(graph_data[graph_name], start, goal)
print(f"BFS path from {start} to {goal}: {path}")

path = hill_climbing(graph_data[graph_name], start, goal, heuristic_key)
print(f"Hill Climbing path from {start} to {goal}: {path}")

path = best_first_search(graph_data[graph_name], start, goal, heuristic_key)
print(f"Best First Search path from {start} to {goal}: {path}")


#NOTE:fix it error with graph2
path, cost = beam_search(graph_data[graph_name], start, goal, heuristic_key, beam_width)
print("Beam Search path:", path)
print("Total path cost:", cost)

path, cost = branch_and_bound(graph_data[graph_name], start, goal)
print("Branch and Bound path:", path)
print("Total path cost:", cost)

path, cost = branch_and_bound_extended(graph_data[graph_name], start, goal)
print("Branch and Bound (Extended Set) path:", path)
print("Total path cost:", cost)

path, cost = branch_and_bound_heuristic(graph_data[graph_name], start, goal, heuristic_key)
print("Branch and Bound with Heuristics path:", path)
print("Total path cost:", cost)

#NOTE: fix it error with graph3
path, cost = a_star_search(graph_data[graph_name], start, goal, heuristic_key)
print("A* Search path:", path)
print("Total path cost:", cost)