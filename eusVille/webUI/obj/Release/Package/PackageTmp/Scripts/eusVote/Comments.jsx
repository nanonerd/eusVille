var CommentBox = React.createClass({		
	getInitialState: function(){	
		var self = this;	// Need to declare here, not in e.g., componentDidMount
		return {data: this.props.initialData};
	},
	loadCommentsFromServer: function(){	
		var url = this.props.url + self.answerID;
		var result = eusAPI.get(url);
		this.setState({ data: result});	
	},
	handleCommentSubmit: function(comment){
		// submit to the server and refresh the list
		// Create json array with UserID, AnswerID, and Rating
				
		var comments = this.state.data;
		var newComments = comments.concat([comment]);
	    this.setState({data: newComments});


    var obj = {};
    obj["UserID"] = self.userID;
    obj["AnswerID"] = self.answerID;   // $(this).data('id');
    
    obj["Comment"] = comment.Text;

    // Create json string
    var data = JSON.stringify(obj);

    // Create json string with Filter to send to API
    var jsonString = JSON.stringify({ "Filter": "CommentPost", "FilterData": data });    

    var result = eusAPI.post(this.props.submitUrl, jsonString);
								
	},	
	componentDidMount: function(){
		// Get hidden field values.
		self.userID = document.getElementById('UserID').value;
		self.answerID = $('#AnswerID').val();						
		self.answerTitle = $('#AnswerTitle').val();
		
		$('#answerTitle').text(self.answerTitle);   // Set comment title
		this.loadCommentsFromServer();
		//window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
	},
	render: function(){		
		return (
			<div className="commentBox">				
				<h2>Comments: <span id='answerTitle'></span></h2>				
				<CommentList data={this.state.data} />
				<CommentForm onCommentSubmit={this.handleCommentSubmit} />
			</div>
		);
	}
});

var CommentList = React.createClass({
	render: function(){
		var commentNodes = this.props.data.map(function(comment){
			return(
				<ul>
				<Comment author={comment.Author}>
					{comment.Text}
				</Comment>
				</ul>
			);
		});
		return (
			<div className="commentList">
				{commentNodes}
			</div>
		);	
	}
});

var CommentForm = React.createClass({
	handleSubmit: function(e) {
		e.preventDefault();		
		var author = this.refs.author.getDOMNode().value.trim();
		var text = this.refs.text.getDOMNode().value.trim();
		if (!text || !author){
			return;
		}
		// todo: send request to the server
		this.props.onCommentSubmit({Author: author, Text: text});

		// reset the fields to blanks
		this.refs.author.getDOMNode().value='';
		this.refs.text.getDOMNode().value = '';
		return;
	},
	render: function(){
		return (
			<form className="commentForm" onSubmit={this.handleSubmit}>
				<input type="text" placeholder="your name" ref="author"/>
				<input type="text" placeholder="say something" ref="text"/>
				<input type="submit" value="Post" />
			</form>
		);	
	}
});

var Comment = React.createClass({
	render: function(){
		var auth = this.props.author.toString();
		var converter = new Showdown.converter();
		var rawMarkup = converter.makeHtml(this.props.children.toString());
		return (
			<div className="comment">
				<h2 className="commentAuthor">
					{this.props.author}
				</h2>
					<span dangerouslySetInnerHTML={{__html: rawMarkup}} />
			</div>
		);
	}
});

//React.render(
//	<CommentBox url="/comments/get/" submitUrl="/eusVote_Topic/Topic/" pollInterval={2000} />, document.getElementById('content')
//);